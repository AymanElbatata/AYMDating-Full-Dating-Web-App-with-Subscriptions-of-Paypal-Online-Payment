using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Contexts;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace AYMDating.API.Helpers
{
    public class UpdatedCurrentUserPackage : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IUnitOfWork unitOfWork;

        public UpdatedCurrentUserPackage(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task UpdatedCurrentUserPackageMethod()
        {

            var AllUsers = await GetAllValidUsers();
            foreach (var item in AllUsers)
            {
                var userPackageExpire = unitOfWork.UserPackageRepository.FindByQuery(a => a.AppUserId == item.Id && a.PackageId != 1 && a.IsDeleted == false && DateTime.Now > a.PackageEndDate).FirstOrDefault();
                if (userPackageExpire != null)
                {
                    var NewuserPackage = userPackageExpire;
                    NewuserPackage.PackageId = 1;
                    await unitOfWork.UserPackageRepository.Add(NewuserPackage);

                    userPackageExpire.IsDeleted = true;
                    await unitOfWork.UserPackageRepository.Update(userPackageExpire);
                }
            }

        }

        private  async Task<List<AppUser>> GetAllValidUsers()
        {
            var AllValidUsers = new List<AppUser>();
            var AllUsers = unitOfWork.UserManager.Users.ToList().OrderByDescending(a => a.DateOfJoin);
            foreach (var item in AllUsers)
            {
                if (await MethodGetIsUserValidByUserId(item.Id))
                {
                    AllValidUsers.Add(item);
                }
            }
            return AllValidUsers;
        }

        private async Task<bool> MethodGetIsUserValidByUserId(string UserId)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(UserId);
            bool IsUser = user != null ? await unitOfWork.UserManager.IsInRoleAsync(user, "User") : false;
            bool IsValidUser = user != null && user.IsStopped == false && user.IsDeleted == false && user.Activated == true ? true : false;
            bool IsNotSwitchedOff = user != null && unitOfWork.UserHistoryRepository.FindByQuery(a => a.IsMain == true && a.AppUserId == UserId && a.IsDeleted == false).FirstOrDefault().IsSwitchedOff == false ? false : true;
            if (IsValidUser && IsUser && !IsNotSwitchedOff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        // This method is called when the application stops
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            // Call your method here
            UpdatedCurrentUserPackageMethod().GetAwaiter().GetResult();
        }
    }
}
