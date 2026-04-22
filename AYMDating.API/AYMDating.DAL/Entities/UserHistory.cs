using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserHistory : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public int? CountryId { get; set; }
        public int? LanguageId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? JobId { get; set; }
        public int? PurposeId { get; set; }
        public int? FinancialModeId { get; set; }
        public int? EducationId { get; set; }
        public int? UserPackageId { get; set; }

        public string City { get; set; } = null!;
        public string ProfileHeading { get; set; } = null!;
        public string AboutYou { get; set; } = null!;
        public string AboutPartner { get; set; } = null!;
        public bool IsMain { get; set; } = true;
        public bool IsSwitchedOff { get; set; } = false;

        public int? SearchAgeFrom { get; set; }
        public int? SearchAgeTo { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Language? Language { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual MaritalStatus? MaritalStatus { get; set; }
        public virtual Job? Job { get; set; }
        public virtual Purpose? Purpose { get; set; }
        public virtual FinancialMode? FinancialMode { get; set; }
        public virtual Education? Education { get; set; }
        public virtual UserPackage? UserPackage { get; set; }



    }
}
