namespace AYMDating.API.DTO
{
    public class UserMainSearchDTO
    {
        public int? IamGender { get; set; }
        public int? IwantGender { get; set; }
        public int? Country { get; set;}
        public int? AgeFrom { get; set;}
        public int? AgeTo { get; set;}
        public bool UserHasImage { get; set; }

    }
}
