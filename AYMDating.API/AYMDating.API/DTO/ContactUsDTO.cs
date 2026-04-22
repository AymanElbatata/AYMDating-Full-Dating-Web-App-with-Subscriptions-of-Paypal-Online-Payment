using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class ContactUsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(600)]
        public string Message { get; set; }
    }
}
