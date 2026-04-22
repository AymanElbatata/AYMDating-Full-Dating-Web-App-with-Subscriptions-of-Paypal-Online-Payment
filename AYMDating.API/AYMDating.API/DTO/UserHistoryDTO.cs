using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;

namespace AYMDating.API.DTO
{
    public class UserHistoryDTO
    {
        //public string? AppUserId { get; set; } = null!;
        //public string? Country { get; set; } = null!;
        //public string? Language { get; set; } = null!;
        //public string? Gender { get; set; } = null!;
        //public string? MaritalStatus { get; set; } = null!;
        //public string? Job { get; set; } = null!;
        //public string? Purpose { get; set; } = null!;
        //public string? FinancialMode { get; set; } = null!;
        //public string? Education { get; set; } = null!;
        //public int? Age { get; set; }
        //public string? UserPackage { get; set; } = null!;


        //public string City { get; set; } = null!;
        //public string AboutYou { get; set; } = null!;
        //public string AboutPartner { get; set; } = null!;


        public int? Age { get; set; }
        public int? CounterOfNewMessages { get; set; }
        public int? CounterOfNewLikes { get; set; }
        public int? CounterOfNewViews { get; set; }
        public int? CounterOfNewFavorites { get; set; }
        public int? CounterOfNewBlocks { get; set; }

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

        public string? City { get; set; } = null!;
        public string? ProfileHeading { get; set; } = null!;
        public string? AboutYou { get; set; } = null!;
        public string? AboutPartner { get; set; } = null!;
        public bool IsMain { get; set; } = true;
        public bool IsSwitchedOff { get; set; } = false;

        public int? SearchAgeFrom { get; set; }
        public int? SearchAgeTo { get; set; }

        public bool IsLikedFromCurrentUser { get; set; } = false;
        public bool IsBlockedFromCurrentUser { get; set; } = false;
        public bool IsFavoriteFromCurrentUser { get; set; } = false;
        public bool IsCurrentUserOnline { get; set; } = false;
        public bool IsCurrentUserVerified { get; set; } = false;


        public virtual Country? Country { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual Language? Language { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual MaritalStatus? MaritalStatus { get; set; }
        public virtual Job? Job { get; set; }
        public virtual Purpose? Purpose { get; set; }
        public virtual FinancialMode? FinancialMode { get; set; }
        public virtual Education? Education { get; set; }
        public virtual UserPackage? UserPackage { get; set; }


        public string? MainImage { get; set; } = null!;
        public List<UserImagesDTO>? OtherImages { get; set; } = new List<UserImagesDTO>();

    }
}
