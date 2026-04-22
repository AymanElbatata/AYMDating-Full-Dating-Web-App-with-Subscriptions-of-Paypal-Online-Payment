using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Contexts
{
    public class AYMDatingContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public AYMDatingContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            
        }

        public AYMDatingContext(DbContextOptions<AYMDatingContext> options) : base(options)
        {

        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Education> Educations { get; set; } = null!;
        public virtual DbSet<FinancialMode> FinancialModes { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;
        public virtual DbSet<Purpose> Purposes { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<UserHistory> UserHistories { get; set; } = null!;
        public virtual DbSet<UserImage> UserImages { get; set; } = null!;
        public virtual DbSet<SendingEmail> SendingEmails { get; set; } = null!;
        public virtual DbSet<ActivateUser> ActivateUsers { get; set; } = null!;
        public virtual DbSet<ForgotPasswordUser> ForgotPasswordUsers { get; set; } = null!;
        public virtual DbSet<UserMessage> UserMessages { get; set; } = null!;
        public virtual DbSet<UserFavorite> UserFavorites { get; set; } = null!;
        public virtual DbSet<UserLike> UserLikes { get; set; } = null!;
        public virtual DbSet<UserView> UserViews { get; set; } = null!;
        public virtual DbSet<UserBlock> UserBlocks { get; set; } = null!;
        public virtual DbSet<UserMessageGroup> UserMessageGroups { get; set; } = null!;
        public virtual DbSet<UserReport> UserReports { get; set; } = null!;
        public virtual DbSet<UserPackage> UserPackages { get; set; } = null!;
        public virtual DbSet<UserTokenTransaction> UserTokenTransactions { get; set; } = null!;
        public virtual DbSet<UserPayment> UserPayments { get; set; } = null!;
        public virtual DbSet<ContactUs> ContactUss { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("server=.; database=AYMDatingDB; trusted_connection = true;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BDataSchema");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("Users", "ASecurity");
            modelBuilder.Entity<AppRole>().ToTable("Roles", "ASecurity");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "ASecurity");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "ASecurity");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "ASecurity");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "ASecurity");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "ASecurity");
        }
    }
}
