using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AYMDating.DAL.Contexts
{
    public class AYMDatingContextSeed
    {
        public static async Task SeedAsync(AYMDatingContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,  ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Countries.Any())
                {
                    var CountryData = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Country.json");
                    var Countries = JsonSerializer.Deserialize<List<Country>>(CountryData);

                    for (int i = 0; i < Countries?.Count; i++)
                    {
                        context.Countries.Add(Countries[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Educations.Any())
                {
                    var EducationData = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Education.json");
                    var Education = JsonSerializer.Deserialize<List<Education>>(EducationData);

                    for (int i = 0; i < Education?.Count; i++)
                    {
                        context.Educations.Add(Education[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Genders.Any())
                {
                    var GenderData = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Gender.json");
                    var Gender = JsonSerializer.Deserialize<List<Gender>>(GenderData);

                    for (int i = 0; i < Gender?.Count; i++)
                    {
                        context.Genders.Add(Gender[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Packages.Any())
                {
                    var PackageData = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Package.json");
                    var Package = JsonSerializer.Deserialize<List<Package>>(PackageData);

                    for (int i = 0; i < Package?.Count; i++)
                    {
                        context.Packages.Add(Package[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.FinancialModes.Any())
                {
                    var FinancialMode = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/FinancialMode.json");
                    var FMode = JsonSerializer.Deserialize<List<FinancialMode>>(FinancialMode);

                    for (int i = 0; i < FMode?.Count; i++)
                    {
                        context.FinancialModes.Add(FMode[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Jobs.Any())
                {
                    var Jobs = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Job.json");
                    var Job = Jobs.Split(',').ToList();
                    //var Job = JsonSerializer.Deserialize<List<Job>>(jobsArr);

                    for (int i = 0; i < Job?.Count; i++)
                    {
                        Job Newjob = new Job { Name = Job[i].Split("\"")[1] };
                        context.Jobs.Add(Newjob);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.MaritalStatuses.Any())
                {
                    var MaritalStatus = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/MaritalStatus.json");
                    var MStatus = JsonSerializer.Deserialize<List<MaritalStatus>>(MaritalStatus);

                    for (int i = 0; i < MStatus?.Count; i++)
                    {
                        context.MaritalStatuses.Add(MStatus[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Purposes.Any())
                {
                    var Purposes = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Purpose.json");
                    var Purpose = JsonSerializer.Deserialize<List<Purpose>>(Purposes);

                    for (int i = 0; i < Purpose?.Count; i++)
                    {
                        context.Purposes.Add(Purpose[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Languages.Any())
                {
                    var Languages = File.ReadAllText("../AYMDating.DAL/Contexts/SeedData/Language.json");
                    var Language = JsonSerializer.Deserialize<List<Language>>(Languages);

                    for (int i = 0; i < Language?.Count; i++)
                    {
                        context.Languages.Add(Language[i]);
                    }
                    await context.SaveChangesAsync();
                }

                if (!roleManager.Roles.Any())
                {
                    var role1 = new AppRole
                    {
                        Name = "Admin"
                    };
                    var role2 = new AppRole
                    {
                        Name = "User"
                    };

                    await roleManager.CreateAsync(role1);
                    await roleManager.CreateAsync(role2);
                }

                if (!userManager.Users.Any())
                {
                    var user1 = new AppUser
                    {
                        NormalizedUserName = "AymanElbatata".ToUpper(),
                        Email = "Ayman.Fathy.Elbatata@gmail.com",
                        UserName = "Ayman.Elbatata",
                        IsStopped = false,
                        Activated = true,
                        DateOfBirth = new DateTime(1980, 01, 01),
                        LockoutEnabled = false,
                        PhoneNumber = "201284878483"
                    };
                    var user2 = new AppUser
                    {
                        NormalizedUserName = "AymanFathy".ToUpper(),
                        Email = "Ayman_Elbatata@outlook.com",
                        UserName = "Ayman.Fathy",
                        IsStopped = false,
                        Activated = true,
                        DateOfBirth = new DateTime(1982, 02, 01),
                        LockoutEnabled = false,
                        PhoneNumber = "201202025251"
                    };
                    //var user3 = new AppUser
                    //{
                    //    NormalizedUserName = "NoraAli".ToUpper(),
                    //    Email = "NoraAli@yahoo.com",
                    //    UserName = "Nora.Ali",
                    //    IsStopped = false,
                    //    Activated = true,
                    //    DateOfBirth = new DateTime(1983, 03, 01),
                    //    LockoutEnabled = false,
                    //    PhoneNumber = "201284878483"
                    //};
                    //var user4 = new AppUser
                    //{
                    //    NormalizedUserName = "SamiaAdel".ToUpper(),
                    //    Email = "SamiaAdel@yahoo.com",
                    //    UserName = "Samia.Adel",
                    //    IsStopped = false,
                    //    Activated = true,
                    //    DateOfBirth = new DateTime(1984, 04, 02),
                    //    LockoutEnabled = false,
                    //    PhoneNumber = "201114770520"
                    //};

                    //var user5 = new AppUser
                    //{
                    //    NormalizedUserName = "SaraSayed".ToUpper(),
                    //    Email = "SaraSayed@yahoo.com",
                    //    UserName = "Sara.Sayed",
                    //    IsStopped = false,
                    //    Activated = true,
                    //    DateOfBirth = new DateTime(1985, 05, 02),
                    //    LockoutEnabled = false,
                    //    PhoneNumber = "201114770522"
                    //};

                    //var user6 = new AppUser
                    //{
                    //    NormalizedUserName = "SalwaSawiras".ToUpper(),
                    //    Email = "SalwaSawiras@yahoo.com",
                    //    UserName = "Salwa.Sawiras",
                    //    IsStopped = false,
                    //    Activated = true,
                    //    DateOfBirth = new DateTime(1986, 06, 02),
                    //    LockoutEnabled = false,
                    //    PhoneNumber = "201114770599"
                    //};
                    //var user7 = new AppUser
                    //{
                    //    NormalizedUserName = "AhmedGamal".ToUpper(),
                    //    Email = "AhmedGamal@yahoo.com",
                    //    UserName = "Ahmed.Gamal",
                    //    IsStopped = false,
                    //    Activated = true,
                    //    DateOfBirth = new DateTime(1987, 02, 02),
                    //    LockoutEnabled = false,
                    //    PhoneNumber = "201114770555"
                    //};

                    await userManager.CreateAsync(user1, "Aym@55555");
                    await userManager.CreateAsync(user2, "Aym@55555");
                    //await userManager.CreateAsync(user3, "Aym@19971");
                    //await userManager.CreateAsync(user4, "Aym@19971");
                    //await userManager.CreateAsync(user5, "Aym@19971");
                    //await userManager.CreateAsync(user6, "Aym@19971");
                    //await userManager.CreateAsync(user7, "Aym@19971");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "User");
                    //await userManager.AddToRoleAsync(user3, "User");
                    //await userManager.AddToRoleAsync(user4, "User");
                    //await userManager.AddToRoleAsync(user5, "User");
                    //await userManager.AddToRoleAsync(user6, "User");
                    //await userManager.AddToRoleAsync(user7, "User");
                    await context.SaveChangesAsync();

                    var userPackage1 = new UserPackage
                    {
                        AppUserId = user1.Id,
                        PackageId = 1,
                        PackageEndDate = DateTime.Now.AddYears(20),
                    };
                    var userPackage2 = new UserPackage
                    {
                        AppUserId = user2.Id,
                        PackageId = 2,
                        PackageEndDate = DateTime.Now.AddMinutes(10),
                    };
                    //var userPackage3 = new UserPackage
                    //{
                    //    AppUserId = user3.Id,
                    //    PackageId = 1,
                    //    PackageEndDate = DateTime.Now.AddMonths(1),
                    //};

                    //var userPackage4 = new UserPackage
                    //{
                    //    AppUserId = user4.Id,
                    //    PackageId = 2,
                    //    PackageEndDate = DateTime.Now.AddMonths(1),
                    //};
                    //var userPackage5 = new UserPackage
                    //{
                    //    AppUserId = user5.Id,
                    //    PackageId = 3,
                    //    PackageEndDate = DateTime.Now.AddMonths(1),
                    //};
                    //var userPackage6 = new UserPackage
                    //{
                    //    AppUserId = user6.Id,
                    //    PackageId = 4,
                    //    PackageEndDate = DateTime.Now.AddMonths(1),
                    //};
                    //var userPackage7 = new UserPackage
                    //{
                    //    AppUserId = user7.Id,
                    //    PackageId = 5,
                    //    PackageEndDate = DateTime.Now.AddMonths(1),
                    //};
                    context.UserPackages.Add(userPackage1);
                    context.UserPackages.Add(userPackage2);
                    //context.UserPackages.Add(userPackage3);
                    //context.UserPackages.Add(userPackage4);
                    //context.UserPackages.Add(userPackage5);
                    //context.UserPackages.Add(userPackage6);
                    //context.UserPackages.Add(userPackage7);
                    await context.SaveChangesAsync();

                    var userhistory1 = new UserHistory
                    {
                        AppUserId = user1.Id,
                        SearchAgeFrom = 18,
                        SearchAgeTo = 40,
                        CountryId = 1,
                        EducationId = 1,
                        FinancialModeId = 1,
                        GenderId = 1,                          
                        JobId = 1,
                        UserPackageId = context.UserPackages.Where(a => a.AppUserId == user1.Id && a.IsDeleted == false).FirstOrDefault().ID,
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

                    var userhistory2 = new UserHistory
                    {
                        AppUserId = user2.Id,
                        SearchAgeFrom =20,
                        SearchAgeTo = 55,
                        CountryId = 1,
                        EducationId = 1,
                        FinancialModeId = 1,
                        GenderId = 1,
                        JobId = 1,
                        UserPackageId = context.UserPackages.Where(a => a.AppUserId == user2.Id && a.IsDeleted == false).FirstOrDefault().ID,
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
                    //var userhistory3 = new UserHistory
                    //{
                    //    AppUserId = user3.Id,
                    //    SearchAgeFrom = 21,
                    //    SearchAgeTo = 70,
                    //    CountryId = 2,
                    //    EducationId = 2,
                    //    FinancialModeId = 2,
                    //    GenderId = 2,
                    //    JobId = 2,
                    //    UserPackageId = context.UserPackages.Where(a => a.AppUserId == user3.Id && a.IsDeleted == false).FirstOrDefault().ID,
                    //    ProfileHeading = "ProfileHeading0123456789",
                    //    AboutPartner = "AboutPartnerTest0123456789",
                    //    AboutYou = "AboutYouTest0123456789",
                    //    City = "CityTest0123456789",
                    //    IsMain = true,
                    //    LanguageId = 2,
                    //    MaritalStatusId = 2,
                    //    PurposeId = 2,
                    //    IsSwitchedOff = false
                    //};

                    //var userhistory4 = new UserHistory
                    //{
                    //    AppUserId = user4.Id,
                    //    SearchAgeFrom = 23,
                    //    SearchAgeTo = 56,
                    //    CountryId = 3,
                    //    EducationId = 3,
                    //    FinancialModeId = 3,
                    //    GenderId = 2,
                    //    JobId = 3,
                    //    UserPackageId = context.UserPackages.Where(a => a.AppUserId == user4.Id && a.IsDeleted == false).FirstOrDefault().ID,
                    //    ProfileHeading = "ProfileHeading0123456789",
                    //    AboutPartner = "AboutPartnerTest0123456789",
                    //    AboutYou = "AboutYouTest0123456789",
                    //    City = "CityTest0123456789",
                    //    IsMain = true,
                    //    LanguageId = 3,
                    //    MaritalStatusId = 3,
                    //    PurposeId = 3,
                    //    IsSwitchedOff = false
                    //};

                    //var userhistory5 = new UserHistory
                    //{
                    //    AppUserId = user5.Id,
                    //    SearchAgeFrom = 19,
                    //    SearchAgeTo = 66,
                    //    CountryId = 4,
                    //    EducationId = 4,
                    //    FinancialModeId = 4,
                    //    GenderId = 2,
                    //    JobId = 4,
                    //    UserPackageId = context.UserPackages.Where(a => a.AppUserId == user5.Id && a.IsDeleted == false).FirstOrDefault().ID,
                    //    ProfileHeading = "ProfileHeading0123456789",
                    //    AboutPartner = "AboutPartnerTest0123456789",
                    //    AboutYou = "AboutYouTest0123456789",
                    //    City = "CityTest0123456789",
                    //    IsMain = true,
                    //    LanguageId = 4,
                    //    MaritalStatusId = 4,
                    //    PurposeId = 1,
                    //    IsSwitchedOff = false
                    //};
                    //var userhistory6 = new UserHistory
                    //{
                    //    AppUserId = user6.Id,
                    //    SearchAgeFrom = 27,
                    //    SearchAgeTo = 58,
                    //    CountryId = 5,
                    //    EducationId = 5,
                    //    FinancialModeId = 4,
                    //    GenderId = 2,
                    //    JobId = 5,
                    //    UserPackageId = context.UserPackages.Where(a => a.AppUserId == user6.Id && a.IsDeleted == false).FirstOrDefault().ID,
                    //    ProfileHeading = "ProfileHeading0123456789",
                    //    AboutPartner = "AboutPartnerTest0123456789",
                    //    AboutYou = "AboutYouTest0123456789",
                    //    City = "CityTest0123456789",
                    //    IsMain = true,
                    //    LanguageId = 5,
                    //    MaritalStatusId = 5,
                    //    PurposeId = 5,
                    //    IsSwitchedOff = false
                    //};
                    //var userhistory7 = new UserHistory
                    //{
                    //    AppUserId = user7.Id,
                    //    SearchAgeFrom = 28,
                    //    SearchAgeTo = 59,
                    //    CountryId = 6,
                    //    EducationId = 6,
                    //    FinancialModeId = 6,
                    //    GenderId = 1,
                    //    JobId = 6,
                    //    UserPackageId = context.UserPackages.Where(a => a.AppUserId == user7.Id && a.IsDeleted == false).FirstOrDefault().ID,
                    //    ProfileHeading = "ProfileHeading0123456789",
                    //    AboutPartner = "AboutPartnerTest0123456789",
                    //    AboutYou = "AboutYouTest0123456789",
                    //    City = "CityTest0123456789",
                    //    IsMain = true,
                    //    LanguageId = 6,
                    //    MaritalStatusId = 6,
                    //    PurposeId = 6,
                    //    IsSwitchedOff = false
                    //};
                    context.UserHistories.Add(userhistory1);
                    context.UserHistories.Add(userhistory2);
                    //context.UserHistories.Add(userhistory3);
                    //context.UserHistories.Add(userhistory4);
                    //context.UserHistories.Add(userhistory5);
                    //context.UserHistories.Add(userhistory6);
                    //context.UserHistories.Add(userhistory7);
                    await context.SaveChangesAsync();
                    
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AYMDatingContext>();
                logger.LogError(ex.Message);
            }
        }



    }
}
