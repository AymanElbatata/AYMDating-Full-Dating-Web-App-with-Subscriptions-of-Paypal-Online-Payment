using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class UserViewOrLikeOrFavoriteOrBlockOrReportDTO
    {
        //[Required]
        //[EmailAddress]
        public int? ID { get; set; }
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public DateTime? CreationDate { get; set; }

    }
}
