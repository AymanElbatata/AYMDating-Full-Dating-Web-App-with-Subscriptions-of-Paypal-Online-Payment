using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.IRepositories;
using AYMDating.DAL.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        SignInManager<AppUser> SignInManager { get;  }
        RoleManager<AppRole> RoleManager { get;  }
        UserManager<AppUser> UserManager { get;  }
        ITokenService TokenService { get;  }
        IAppSession AppSession { get; }

        IJobRepository JobRepository { get; }
        IPackageRepository PackageRepository { get; }
        IUserHistoryRepository UserHistoryRepository { get; }
        IActivateUserRepository ActivateUserRepository { get; }
        IUserImageRepository UserImageRepository { get; }
        IUserMessageRepository UserMessageRepository { get; }
        IUserFavoriteRepository UserFavoriteRepository { get; }
        IUserLikeRepository UserLikeRepository { get; }
        IUserViewRepository UserViewRepository { get; }
        IUserBlockRepository UserBlockRepository { get; }
        IUserMessageGroupRepository UserMessageGroupRepository { get; }
        IUserReportRepository UserReportRepository { get; }
        IUserPackageRepository UserPackageRepository { get; }
        ICountryRepository CountryRepository { get; }
        IGenderRepository GenderRepository { get; }
        IEducationRepository EducationRepository { get; }
        IMaritalStatusRepository MaritalStatusRepository { get; }
        IPurposeRepository PurposeRepository { get; }
        IFinancialModeRepository FinancialModeRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IUserTokenTransactionRepository UserTokenTransactionRepository { get; }
        IForgotPasswordUserRepository ForgotPasswordUserRepository { get; }
        IUserPaymentRepository UserPaymentRepository { get; }

        IContactUsRepository ContactUsRepository { get; }
    }
}
