using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.API.Helpers;
using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;

namespace AYMDating.API.Controllers
{
    public class PaymentsController : BaseAuthorizedController
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly PayPalService _payPalService;

        public PaymentsController(IUnitOfWork unitOfWork, PayPalService payPalService, IConfiguration configuration)
        {
            _payPalService = payPalService;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpPost("CreatePayment")]
        public IActionResult CreatePayment([FromBody] int amount)
        {
            try
            {
                //var redirectUrl = $"{Request.Scheme}://{Request.Host}/api/payments";
                var redirectUrl = configuration["BlazorUrl"];
                var payment = _payPalService.CreatePayment(redirectUrl, amount);
                var approvalUrl = payment.links.FirstOrDefault(link => link.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
                return Ok(new { approvalUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ExecutePaymentAndSaving")]
        public async Task<ActionResult<ReturnValueDTO>> ExecutePaymentAndSaving([FromBody] UserPaymentDTO model)
        {
            try
            {
                //var x = unitOfWork.AppSession.User1[0];
                int packageId = 1;
                DateTime PackageEndDate = DateTime.Now;

                var paymentResult = _payPalService.ExecutePayment(model.PaymentId, model.PayerId);

                // Save the payment result in the database
                if (paymentResult.token != null)
                {
                    var payment = new UserPayment
                    {
                        AppUserId = model.AppUserId,
                        PaymentId = paymentResult.id,
                        PayerId = model.PayerId,
                        token = model.token,
                        Amount = int.Parse(paymentResult.transactions[0].amount.total),  // Store amount in dollars
                        PaymentDate = DateTime.Now,
                        Status = paymentResult.state
                    };

                    await unitOfWork.UserPaymentRepository.Add(payment);

                    if (payment.Amount > 10 && payment.Amount < 15)
                    {
                        packageId = 2;
                        PackageEndDate = DateTime.Now.AddDays(7);
                    }
                    else if (payment.Amount > 25 && payment.Amount < 35)
                    {
                        packageId = 3;
                        PackageEndDate = DateTime.Now.AddDays(30);
                    }
                    else if (payment.Amount > 50 && payment.Amount < 60)
                    {
                        packageId = 4;
                        PackageEndDate = DateTime.Now.AddDays(180);
                    }
                    else if (payment.Amount > 100 && payment.Amount < 120)
                    {
                        packageId = 5;
                        PackageEndDate = DateTime.Now.AddDays(360);
                    }

                    var newUserPackage = new UserPackage
                    {
                        AppUserId = model.AppUserId,
                        PackageId = packageId,
                        PackageEndDate = PackageEndDate
                    };
                    var OldUserPackage = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == model.AppUserId && a.IsDeleted == false).FirstOrDefault();
                    OldUserPackage.IsDeleted = true;
                    await unitOfWork.UserPackageRepository.Update(OldUserPackage);
                    await unitOfWork.UserPackageRepository.Add(newUserPackage);

                    var OldUserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a=>a.AppUserId == model.AppUserId && a.IsDeleted == false && a.IsMain == true).FirstOrDefault();
                    OldUserHistory.UserPackageId = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == model.AppUserId && a.IsDeleted == false).FirstOrDefault().ID;
                    await unitOfWork.UserHistoryRepository.Update(OldUserHistory);

                    var email = new SendingEmail()
                    {
                        Title = "SomeOne Did Buy a Package - AYM DATING APP",
                        Body = "\n" + "UserId: " + "\n" + model.AppUserId + "\n" 
                        + "And His UserName: " + OldUserHistory.AppUser.UserName
                        + "\n" + "And UserName: " + OldUserHistory.AppUser.Email
                        + "\n" + "And PackageName: " + newUserPackage.Package.Name
                        + "\n" + "And PackageEndDate: " + newUserPackage.PackageEndDate
                        + "\n" + "And Paid: " + payment.Amount,
                        To = "ayman.fathy.elbatata@gmail.com"
                    };
                    SendingEmailSettings.SendEmail(email);


                    return Ok(new ReturnValueDTO(ReturnType.Success, true));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
