using AYMDating.DAL.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace AYMDating.API.DTO
{
    public class UserMessageDTO
    {
        //[Required]
        ////[EmailAddress]
        public int? ID { get; set; }
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public string? Message { get; set; }
        public bool IsSeen { get; set; } = false;
        public bool IsDeletedFromSender { get; set; } = false;
        public bool IsDeletedFromReceiver { get; set; } = false;
        public DateTime? CreationDate { get; set; }
        public virtual AppUser? SenderAppUser { get; set; }

    }
}
