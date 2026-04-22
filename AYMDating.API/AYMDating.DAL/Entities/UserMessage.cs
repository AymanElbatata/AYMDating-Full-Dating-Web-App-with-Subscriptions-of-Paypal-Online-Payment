using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserMessage : BaseEntity<int>
    {
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public string? Message { get; set; } = null!;
        public string? MessageGuid { get; set; }

        public bool IsSeen { get; set; } = false;
        public bool IsDeletedFromSender { get; set; } = false;
        public bool IsDeletedFromReceiver { get; set; } = false;

        public virtual AppUser? SenderAppUser { get; set; }
        public virtual AppUser? ReceiverAppUser { get; set; }
    }
}
