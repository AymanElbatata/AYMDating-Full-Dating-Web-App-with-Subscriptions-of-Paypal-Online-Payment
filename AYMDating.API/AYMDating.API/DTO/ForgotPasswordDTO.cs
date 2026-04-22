using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string Code { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\S)[a-zA-Z\S]{8,100}$", ErrorMessage = "Password Must contain one upper letter and one lower letter.")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password Isn't Match")]
        public string ConfirmPassword { get; set; }

        public string? Token { get; set; }
    }
}
