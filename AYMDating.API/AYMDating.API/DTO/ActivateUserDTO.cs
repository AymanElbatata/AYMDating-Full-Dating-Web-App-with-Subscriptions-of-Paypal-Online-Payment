using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class ActivateUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string Code { get; set; }
    }
}
