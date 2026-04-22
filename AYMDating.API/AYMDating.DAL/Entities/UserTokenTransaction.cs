using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserTokenTransaction : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? Token { get; set; } = null!;
        public DateTime? DateOfMaking { get; set; } 
        public DateTime? DateOfExpiration { get; set; } 
        public virtual AppUser? AppUser { get; set; }
    }
}
