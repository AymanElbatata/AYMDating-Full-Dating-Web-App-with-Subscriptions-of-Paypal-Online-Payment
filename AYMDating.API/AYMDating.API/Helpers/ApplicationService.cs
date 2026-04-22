using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Contexts;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.IRepositories;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AYMDating.API.Helpers
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
             x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});
            //services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //});
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppSession, AppSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
            services.AddScoped<IActivateUserRepository, ActivateUserRepository>();
            services.AddScoped<IForgotPasswordUserRepository, ForgotPasswordUserRepository>();
            services.AddScoped<IUserImageRepository, UserImageRepository>();
            services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            services.AddScoped<IUserViewRepository, UserViewRepository>();
            services.AddScoped<IUserBlockRepository, UserBlockRepository>();
            services.AddScoped<IUserMessageGroupRepository, UserMessageGroupRepository>();
            services.AddScoped<IUserReportRepository, UserReportRepository>();
            services.AddScoped<IUserPackageRepository, UserPackageRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
            services.AddScoped<IFinancialModeRepository, FinancialModeRepository>();
            services.AddScoped<IPurposeRepository, PurposeRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IUserTokenTransactionRepository, UserTokenTransactionRepository>();
            services.AddScoped<IUserPaymentRepository, UserPaymentRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<PayPalService>();

            //services.AddSingleton<IUpdatedCurrentUserPackage, UpdatedCurrentUserPackage>();
            //services.AddSingleton<IHostedService, UpdatedCurrentUserPackage>();

            return services;
        }
    }
}
