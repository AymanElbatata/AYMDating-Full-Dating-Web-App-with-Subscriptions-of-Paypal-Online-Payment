using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\S)[a-zA-Z\S]{8,100}$", ErrorMessage = "Password Must contain one upper letter and one lower letter.")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage ="Password Isn't Match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10,15}$", ErrorMessage = "10 up to 15 Number")]
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Gender { get; set; } = 1;
        //public int UserPackageId { get; set; }
    }
}
