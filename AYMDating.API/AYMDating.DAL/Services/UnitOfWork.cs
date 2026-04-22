using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public SignInManager<AppUser> SignInManager { get; set; }
        public RoleManager<AppRole> RoleManager { get; set; }
        public UserManager<AppUser> UserManager { get; set; }
        public ITokenService TokenService { get; set; }
        public IAppSession AppSession { get; set; }
        public IJobRepository JobRepository { get; set; }
        public IPackageRepository PackageRepository { get; set; }
        public IUserHistoryRepository UserHistoryRepository { get; set; }
        public IActivateUserRepository ActivateUserRepository { get; set; }
        public IUserImageRepository UserImageRepository { get; set; }
        public IUserMessageRepository UserMessageRepository { get; set; }
        public IUserFavoriteRepository UserFavoriteRepository { get; set; }
        public IUserLikeRepository UserLikeRepository { get; set; }
        public IUserViewRepository UserViewRepository { get; set; }
        public IUserBlockRepository UserBlockRepository { get; set; }
        public IUserMessageGroupRepository UserMessageGroupRepository { get; set; }
        public IUserReportRepository UserReportRepository { get; set; }
        public IUserPackageRepository UserPackageRepository { get; set; }
        public ICountryRepository CountryRepository { get; set; }
        public IGenderRepository GenderRepository { get; set; }
        public IEducationRepository EducationRepository { get; set; }
        public IMaritalStatusRepository MaritalStatusRepository { get; set; }
        public IPurposeRepository PurposeRepository { get; set; }
        public IFinancialModeRepository FinancialModeRepository { get; set; }
        public ILanguageRepository LanguageRepository { get; set; }
        public IUserTokenTransactionRepository UserTokenTransactionRepository { get; set; }
        public IForgotPasswordUserRepository ForgotPasswordUserRepository { get; set; }
        public IUserPaymentRepository UserPaymentRepository { get; set; }
        public IContactUsRepository ContactUsRepository { get; set; }

        public UnitOfWork(
            SignInManager<AppUser> SignInManager,
            RoleManager<AppRole> RoleManager,
            UserManager<AppUser> UserManager,
            ITokenService TokenService,
            IAppSession AppSession,
            IJobRepository JobRepository,
            IPackageRepository PackageRepository,
            IUserHistoryRepository UserHistoryRepository,
            IActivateUserRepository ActivateUserRepository,
            IUserImageRepository UserImageRepository,
            IUserMessageRepository UserMessageRepository,
            IUserFavoriteRepository UserFavoriteRepository,
            IUserLikeRepository UserLikeRepository,
            IUserViewRepository UserViewRepository,
            IUserBlockRepository UserBlockRepository,
            IUserMessageGroupRepository UserMessageGroupRepository,
            IUserReportRepository UserReportRepository,
            IUserPackageRepository UserPackageRepository,
            ICountryRepository CountryRepository,
            IGenderRepository GenderRepository,
            IEducationRepository EducationRepository,
            IMaritalStatusRepository MaritalStatusRepository,
            IPurposeRepository PurposeRepository,
            IFinancialModeRepository FinancialModeRepository,
            ILanguageRepository LanguageRepository,
            IUserTokenTransactionRepository UserTokenTransactionRepository,
            IForgotPasswordUserRepository ForgotPasswordUserRepository,
            IUserPaymentRepository UserPaymentRepository,
            IContactUsRepository ContactUsRepository
            )

        {
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
            this.TokenService = TokenService;
            this.JobRepository = JobRepository;
            this.PackageRepository = PackageRepository;
            this.AppSession = AppSession;
            this.UserHistoryRepository = UserHistoryRepository;
            this.ActivateUserRepository = ActivateUserRepository;
            this.UserImageRepository = UserImageRepository;
            this.UserMessageRepository = UserMessageRepository;
            this.UserFavoriteRepository = UserFavoriteRepository;
            this.UserLikeRepository = UserLikeRepository;
            this.UserViewRepository = UserViewRepository;
            this.UserPackageRepository = UserPackageRepository;
            this.UserBlockRepository = UserBlockRepository;
            this.UserMessageGroupRepository = UserMessageGroupRepository;
            this.UserReportRepository = UserReportRepository;
            this.UserPackageRepository = UserPackageRepository;
            this.CountryRepository = CountryRepository;
            this.GenderRepository = GenderRepository;
            this.UserTokenTransactionRepository = UserTokenTransactionRepository;
            this.EducationRepository = EducationRepository;
            this.MaritalStatusRepository = MaritalStatusRepository;
            this.PurposeRepository = PurposeRepository;
            this.FinancialModeRepository = FinancialModeRepository;
            this.LanguageRepository = LanguageRepository;
            this.ForgotPasswordUserRepository = ForgotPasswordUserRepository;
            this.UserPaymentRepository = UserPaymentRepository;
            this.ContactUsRepository = ContactUsRepository;
        }

    }
}
