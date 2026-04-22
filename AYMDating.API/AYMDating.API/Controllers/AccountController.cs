using AutoMapper;
using AYMDating.API.DTO;
using AYMDating.API.Helpers;
using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Services;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AYMDating.API.Controllers
{
    public class AccountController : BaseController
    {
        //private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        //private readonly List<string> UserTokenData1;

        public AccountController(/*IMapper mapper,*/ IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            //this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        //[AllowAnonymous]
        [HttpGet("HelloWorld")]
        public string HelloWorld()
        {
            return "Hello";
        }

        //[AllowAnonymous]
        [HttpGet("DeleteSwitchedOffUserByUserId/{UserEmail}")]
        public async Task<ActionResult<ReturnValueDTO>> DeleteSwitchedOffUserByUserId(string UserEmail)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(UserEmail);

                if (user == null)
                    return Ok(new ReturnValueDTO(ReturnType.Failed, false));

                if (!unitOfWork.UserHistoryRepository.FindByQuery(a=>a.IsMain == true && a.IsDeleted == false && a.IsSwitchedOff == true && a.AppUserId == user.Id).Any())
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, false));
                }

                return Ok(new ReturnValueDTO(ReturnType.Success, await MethodDeleteSwitchedOffUserAccountByUserId(user.Id)));
            }
            catch (Exception ex)
            {
                return Ok(new ReturnValueDTO(ReturnType.Failed, ex.Message));
            }
        }

        //[AllowAnonymous]
        [HttpPost("SendContactUsFromAnonymous")]
        public async Task<ActionResult<ReturnValueDTO>> SendContactUsFromAnonymous(ContactUsDTO model)
        {
            try
            {
                var Message = new ContactUs
                {
                    Email = model.Email,
                    Message = model.Message.Length < 600 ? model.Message : model.Message.Substring(0, 600),
                    Name = model.Name,
                };

                await unitOfWork.ContactUsRepository.Add(Message);

                var email = new SendingEmail()
                {
                    Title = "ContactUs - New Message - AYM DATING APP",
                    Body = "\n" + "Name: " + model.Name + "\n" + "Email: " + model.Email + "\n" + "Message: " + model.Message + "\n" + "DateOfMaking: " + Message.CreationDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                    To = "Ayman_elbatata@outlook.com"
                };
                SendingEmailSettings.SendEmail(email);

                var result = unitOfWork.ContactUsRepository.FindByQuery(a=>a.Name == model.Name && a.Email == model.Email && a.Message == model.Message).Any();
                if (result)
                    return Ok(new ReturnValueDTO(ReturnType.Success, true));

                return Ok(new ReturnValueDTO(ReturnType.Failed, "Error, Please try again later"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ReturnValueDTO>> Login([FromBody] LoginDTO login)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(login.Email);
                if (user == null)
                    return Ok(new ReturnValueDTO(ReturnType.Failed, new UserDTO()));

                if (user.IsStopped == true || user.IsDeleted == true )
                    return Ok(new ReturnValueDTO(ReturnType.Failed, new UserDTO()));

                if (user.Activated == false)
                {
                    var userDTO = new UserDTO
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        UserRole = await GetUserRole(user),
                        IsSwitchedOff = UserIsSwitchedOff(user.Id),
                        IsActivated = user.Activated,
                        Token = null,
                    };

                    return Ok(new ReturnValueDTO(ReturnType.Success, userDTO));
                }

                // Start Update User Package
                var userPackageExpire = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == user.Id && a.PackageId != 1 && a.IsDeleted == false && DateTime.Now > a.PackageEndDate).FirstOrDefault();
                if (userPackageExpire != null)
                {
                    userPackageExpire.IsDeleted = true;
                    await unitOfWork.UserPackageRepository.Update(userPackageExpire);

                    var NewuserPackage = new UserPackage
                    {
                        AppUserId = user.Id,
                        PackageEndDate = DateTime.Now.AddYears(20),
                        PackageId = 1,
                        IsDeleted = false
                    };
                    await unitOfWork.UserPackageRepository.Add(NewuserPackage);

                    var currentuserPackage = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == user.Id && a.IsDeleted == false).FirstOrDefault();
                    var currentUserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.IsMain == true && a.IsDeleted == false && a.AppUserId == user.Id).FirstOrDefault();
                    currentUserHistory.UserPackageId = currentuserPackage.ID;
                    await unitOfWork.UserHistoryRepository.Update(currentUserHistory);
                }
                // End  Update User Package

                if (unitOfWork.UserHistoryRepository.FindByQuery(a => a.IsMain == true && a.AppUserId == user.Id && a.IsDeleted == false).Any())
                {
                    var result = await unitOfWork.SignInManager.CheckPasswordSignInAsync(user, login.Password, false);

                    if (result.Succeeded)
                    {
                        await unitOfWork.SignInManager.SignInAsync(user, isPersistent: login.RememberMe);

                        var userDTO = new UserDTO
                        {
                        UserId = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        UserRole = await GetUserRole(user),
                        IsSwitchedOff = UserIsSwitchedOff(user.Id),
                        IsActivated = user.Activated,
                        Token = await CreateTokenAndSaveIt(user),
                        };

                        return Ok(new ReturnValueDTO(ReturnType.Success, userDTO));
                    }

                }

                return Ok(new ReturnValueDTO(ReturnType.Failed, new UserDTO()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //[AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            try
            {
                if (CheckEmailExistsAsync(register.Email).Result)
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, "This Email Address is already in use"));
                }
                if (register.Password != register.ConfirmPassword)
                {
                    return Ok(new ReturnValueDTO(ReturnType.Failed, "Password doesn't Match Confirm Password"));
                }
                var user = new AppUser
                {
                    Email = register.Email,
                    UserName = register.Email.Split("@")[0],
                    PhoneNumber = register.PhoneNumber,
                    DateOfBirth = register.DateOfBirth,
                    Activated = false
                };
                var AddNewUser = await unitOfWork.UserManager.CreateAsync(user, register.Password);

                if (AddNewUser.Succeeded)
                {
                    // Start UserPackage
                    var UserPackage = new UserPackage
                    {
                        PackageId = 1,
                        AppUserId = user.Id,
                        PackageEndDate = DateTime.Now
                    };
                    // End UserPackage
                    await unitOfWork.UserPackageRepository.Add(UserPackage);


                    var userhistory = new UserHistory
                    {
                        AppUserId = user.Id,
                        SearchAgeFrom = 20,
                        SearchAgeTo = 55,
                        CountryId = 1,
                        EducationId = 1,
                        FinancialModeId = 1,
                        GenderId = register.Gender,
                        JobId = 1,
                        UserPackageId = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == user.Id && a.IsDeleted == false).FirstOrDefault().ID,
                        ProfileHeading = "ProfileHeading0123456789",
                        AboutPartner = "AboutPartnerTest0123456789",
                        AboutYou = "AboutYouTest0123456789",
                        City = "CityTest0123456789",
                        IsMain = true,
                        LanguageId = 1,
                        MaritalStatusId = 1,
                        PurposeId = 1,
                        IsSwitchedOff = false
                    };
                    await unitOfWork.UserHistoryRepository.Add(userhistory);

                    var AddtoRole = await unitOfWork.UserManager.AddToRoleAsync(user, "User");
                    if (AddtoRole.Succeeded)
                    {
                        ActivateUserDTO Activation = await CreateUserctivation(user.Email);

                        var email = new SendingEmail()
                        {
                            Title = "Registration - New User - AYM DATING APP",
                            Body = "\n" + "UserId: " + user.Id + "\n" + "UserEmail: " + user.Email + "\n" + "UserName: " + user.UserName + "\n" + "Gender: " + userhistory.Gender.Name,
                            To = "Ayman_elbatata@outlook.com"
                        };
                        SendingEmailSettings.SendEmail(email);


                        return Ok(new ReturnValueDTO(ReturnType.Success, new ActivateUserDTO() { Email = Activation.Email }));
                        
                    }
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, new UserDTO()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[AllowAnonymous]
        [HttpGet("ActivateAgainForUser/{UserEmail}")]
        public async Task<ActionResult<ReturnValueDTO>> ActivateAgainForUser(string UserEmail)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(UserEmail);
                if (user == null || user.Activated == true)
                    return Ok(new ReturnValueDTO(ReturnType.Failed, "There is no Account is Match!"));

                var CheckOldOne = unitOfWork.ActivateUserRepository.FindByQuery(a => a.AppUserId == user.Id && a.IsConfirmed == false && a.IsDeleted == false).FirstOrDefault();
                if (CheckOldOne != null)
                {
                    CheckOldOne.IsDeleted = true;
                    await unitOfWork.ActivateUserRepository.Update(CheckOldOne);
                }

                if (user != null)
                {
                    ActivateUserDTO Activation = await CreateUserctivation(user.Email);
                    return Ok(new ReturnValueDTO(ReturnType.Success, new ActivateUserDTO() { Email = Activation.Email}));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, "Error, Please try again later!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //[AllowAnonymous]
        [HttpPost("VerifyActivateUser")]
        public async Task<ActionResult<ReturnValueDTO>> VerifyActivateUser(ActivateUserDTO model)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var ActivationCode = unitOfWork.ActivateUserRepository.FindByQuery(a => a.AppUserId == user.Id && a.Code == model.Code && a.IsConfirmed == false && a.IsDeleted == false && a.DateOfExpiration > DateTime.Now).FirstOrDefault();
                    if (ActivationCode != null)
                    {
                        ActivationCode.IsConfirmed = true;
                        ActivationCode.LastUpdateUserID = user.Id;
                        ActivationCode.LastUpdateDate = DateTime.Now;
                        await unitOfWork.ActivateUserRepository.Update(ActivationCode);

                        user.Activated = true;
                        var result = await unitOfWork.UserManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return Ok(new ReturnValueDTO(ReturnType.Success, true));
                        }
                    }
                    else
                    {
                        return Ok(new ReturnValueDTO(ReturnType.Failed, "This Code isn't Valid"));
                    }
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, "This Email isn't Registered"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        ////[AllowAnonymous]
        [HttpGet("ForgetPassword/{UserEmail}")]
        public async Task<ActionResult<ReturnValueDTO>> ForgetPassword(string UserEmail)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(UserEmail);

                if (user == null)
                    return Ok(new ReturnValueDTO(ReturnType.Failed, "There is no Account is Match!"));

                var CheckOldOne = unitOfWork.ForgotPasswordUserRepository.FindByQuery(a => a.AppUserId == user.Id && a.IsConfirmed == false && a.IsDeleted == false && a.DateOfExpiration > DateTime.Now).FirstOrDefault();
                if (CheckOldOne != null)
                {
                    CheckOldOne.IsDeleted = true;
                    await unitOfWork.ForgotPasswordUserRepository.Update(CheckOldOne);
                }

                var token = await unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);

                var ForgotPasswordCode = new ForgotPasswordUser
                {
                    AppUserId = user.Id,
                    DateOfExpiration = DateTime.Now.AddHours(12),
                    Code = MySPECIALGUID.GetUniqueKey(10),
                    Token = token,
                    CreatedUserID = user.Id,
                };
                await unitOfWork.ForgotPasswordUserRepository.Add(ForgotPasswordCode);


                var resetPasswordLink = configuration["BlazorUrl"] + "ResetPassword/" + UserEmail + "/" + ForgotPasswordCode.Code;

                var email = new SendingEmail()
                {
                    Title = "Reset Password - AYM DATING APP",
                    Body = "\n" + "Please Click on or copy and paste the Reset Password link in your browser: " + "\n" + resetPasswordLink + "\n" + "Or Activate Manually with your code: " + ForgotPasswordCode.Code,
                    To = UserEmail,
                    CreatedUserID = user.Id
                };
                SendingEmailSettings.SendEmail(email);
                return Ok(new ReturnValueDTO(ReturnType.Success, new ForgotPasswordDTO() { Email = UserEmail }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //[AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ReturnValueDTO>> ResetPassword(ForgotPasswordDTO model)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                    return Ok(new ReturnValueDTO(ReturnType.Failed, "There is no Account is Match!"));

                var ActivationCode = unitOfWork.ForgotPasswordUserRepository.FindByQuery(a => a.AppUserId == user.Id && a.Code == model.Code && a.IsConfirmed == false && a.IsDeleted == false && a.DateOfExpiration > DateTime.Now).FirstOrDefault();

                if (ActivationCode != null)
                {
                    ActivationCode.IsConfirmed = true;
                    ActivationCode.LastUpdateUserID = user.Id;
                    ActivationCode.LastUpdateDate = DateTime.Now;
                    await unitOfWork.ForgotPasswordUserRepository.Update(ActivationCode);

                    var result = await unitOfWork.UserManager.ResetPasswordAsync(user, ActivationCode.Token, model.Password);

                    if (result.Succeeded)
                        return Ok(new ReturnValueDTO(ReturnType.Success, true));
                }
                return Ok(new ReturnValueDTO(ReturnType.Failed, "Error, Please try again later"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("GetCurrentUserData")]
        public async Task<ActionResult<ReturnValueDTO>> GetCurrentUserData()
        {
            try
            {
                var CurrentUser = await GetUserById(unitOfWork.AppSession.User1[0]);
                var userDTO = new UserDTO
                {
                    UserId = CurrentUser.Id,
                    Email = unitOfWork.AppSession.User1[2],
                    UserName = unitOfWork.AppSession.User1[1],
                    UserRole = unitOfWork.AppSession.User1[4],
                    Token = GetToken()
                };
                return Ok(new ReturnValueDTO(ReturnType.Success, userDTO));


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Authorize]
        [HttpGet("GetUserEmailAndUserNameByUserId/{UserId}")]
        public async Task<ActionResult<ReturnValueDTO>> GetUserEmailAndUserNameByUserId(string UserId)
        {
            try
            {
                var RequiredUser = await GetUserById(UserId);
                var userDTO = new UserDTO
                {
                    UserId = UserId,
                    Email = RequiredUser.Email,
                    UserName = RequiredUser.UserName,
                };
                return Ok(new ReturnValueDTO(ReturnType.Success, userDTO));


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        ////[AllowAnonymous]
        //[Authorize]
        [HttpPost("Logout")]
        public async new Task<ActionResult<ReturnValueDTO>> SignOut()
        {
            try
            {
                var TokenExisting = unitOfWork.UserTokenTransactionRepository.FindByQuery(a => a.Token == GetToken()).FirstOrDefault();
                if (TokenExisting != null)
                {
                    TokenExisting.IsDeleted = true;
                    await unitOfWork.UserTokenTransactionRepository.Update(TokenExisting);
                }
                await unitOfWork.SignInManager.SignOutAsync();
                return Ok(new ReturnValueDTO(ReturnType.Success, ""));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        #region Private Methods

        //public List<string> UserTokenData => GetInfoFromUser();

        //private void GetInfoFromUser()
        //{
        //    string token = GetToken();
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        //        foreach (var item in jsonToken.Claims)
        //        {
        //            UserTokenData1.Add(item.Value);
        //        }
        //        UserTokenData1.Add(token);
        //    }
        //}


        private async Task<bool> CheckEmailExistsAsync(string email)
           => await unitOfWork.UserManager.FindByEmailAsync(email) != null;

        private async Task<ActivateUserDTO> CreateUserctivation(string UserEmail)
        {
            try
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(UserEmail);
                if (user != null)
                {
                    var ActivationCode = new ActivateUser
                    {
                        AppUserId = user.Id,
                        DateOfExpiration = DateTime.Now.AddHours(+24),
                        Code = MySPECIALGUID.GetUniqueKey(10),
                        CreatedUserID = user.Id,
                        LastUpdateUserID = user.Id
                    };
                    await unitOfWork.ActivateUserRepository.Add(ActivationCode);


                    var ActivateUserLink = configuration["BlazorUrl"] + "ActivateAccount/" + user.Email + "/" + ActivationCode.Code;

                    var email = new SendingEmail()
                    {
                        Title = "Activate User - Valid for 24 Hour - AYM DATING APP",
                        Body = "\n" + "Please Click on or copy and paste the activation link in your browser: " + "\n" + ActivateUserLink + "\n" +"Or Activate Manually with your code: "+ ActivationCode.Code ,
                        To = user.Email
                    };
                    SendingEmailSettings.SendEmail(email);
                    return new ActivateUserDTO() {Email=user.Email, Code = ActivationCode.Code };
                }
                return new ActivateUserDTO();
            }
            catch (Exception ex)
            {
                return new ActivateUserDTO();
            }

        }

        private async Task<string> GetUserRole(AppUser user)
        {
            string UserRole = "";
            foreach (var item in unitOfWork.RoleManager.Roles.ToList())
            {
                if (await unitOfWork.UserManager.IsInRoleAsync(user, item.Name))
                {
                    UserRole = item.Name;
                    break;
                }
            }
            return UserRole;
        }

        private bool UserIsSwitchedOff(string UserId)
           => unitOfWork.UserHistoryRepository.FindByQuery(a => a.IsMain == true && a.AppUserId == UserId && a.IsDeleted == false).FirstOrDefault().IsSwitchedOff;
        
        private async Task<AppUser> GetUserById(string UserId)
            => await unitOfWork.UserManager.FindByIdAsync(UserId);

        private async Task<string> CreateTokenAndSaveIt(AppUser user)
        {
            var token = await unitOfWork.TokenService.CreateToken(user);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userToken = new UserTokenTransaction
            {
                AppUserId = user.Id,
                Token = token,
                DateOfMaking = DateTime.Parse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == "DateOfMaking")?.Value),
                DateOfExpiration = DateTime.Parse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == "DateOfExpiration")?.Value),
            };

            await unitOfWork.UserTokenTransactionRepository.Add(userToken);

            return token;
        }

        private string GetToken()
        => !string.IsNullOrEmpty(unitOfWork.AppSession.AuthorizationToken) && unitOfWork.AppSession.AuthorizationToken != "Bearer" ? unitOfWork.AppSession.AuthorizationToken.Split(" ")[1] : string.Empty;

        private bool TokenTblIsExpired(string token)
        {
           var TokenExisting =  unitOfWork.UserTokenTransactionRepository.FindByQuery(a=>a.Token == token).FirstOrDefault();
            if (TokenExisting != null)
            {
                return  DateTime.Now > TokenExisting.DateOfExpiration;
            }
            return true;
        }

        private bool RefuseAnyRequestWithInvalidToken()
            => string.IsNullOrEmpty(unitOfWork.AppSession.AuthorizationToken) || unitOfWork.AppSession.AuthorizationToken == "Bearer" || TokenTblIsExpired(GetToken());

        private string GetUserIdFromToken()
        {
            string token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                string UserId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                if (!string.IsNullOrEmpty(UserId))
                    return UserId;
            }
            return string.Empty;
        }


        private async Task<bool> MethodDeleteSwitchedOffUserAccountByUserId(string UserId)
        {
            var UserHistory = unitOfWork.UserHistoryRepository.FindByQuery(a => a.AppUserId == UserId && a.IsMain == true && a.IsSwitchedOff == true && a.IsDeleted == false).FirstOrDefault();
            if (UserHistory != null)
            {

                UserHistory.IsDeleted = true;
                UserHistory.IsMain = false;
                UserHistory.LastUpdateUserID = UserId;
                UserHistory.LastUpdateDate = DateTime.Now;
                await unitOfWork.UserHistoryRepository.Update(UserHistory);

                var NewUserHistory = new UserHistory
                {
                    AppUserId = UserHistory.AppUserId,
                    SearchAgeFrom = UserHistory.SearchAgeFrom,
                    SearchAgeTo = UserHistory.SearchAgeTo,
                    CountryId = UserHistory.CountryId,
                    EducationId = UserHistory.EducationId,
                    FinancialModeId = UserHistory.FinancialModeId,
                    GenderId = UserHistory.GenderId,
                    JobId = UserHistory.JobId,
                    UserPackageId = UserHistory.UserPackageId,
                    ProfileHeading = UserHistory.ProfileHeading,
                    AboutPartner = UserHistory.AboutPartner,
                    AboutYou = UserHistory.AboutYou,
                    City = UserHistory.City,
                    LanguageId = UserHistory.LanguageId,
                    MaritalStatusId = UserHistory.MaritalStatusId,
                    PurposeId = UserHistory.PurposeId,
                    IsMain = true,
                    IsSwitchedOff = false,
                    CreatedUserID = UserId,
                    CreationDate = DateTime.Now
                };

                await unitOfWork.UserHistoryRepository.Add(NewUserHistory);
                return true;
            }
            return false;
        }
        #endregion
    }
}
