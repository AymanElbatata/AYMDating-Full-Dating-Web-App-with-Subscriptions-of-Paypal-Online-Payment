namespace AYMDating.API.DTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public bool IsSwitchedOff { get; set; } = false;
        public bool IsActivated { get; set; } = false;
        public string? Token { get; set; }
        public bool TokenIsExpired { get; set; } = false;


    }
}
