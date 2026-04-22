using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserImage : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? LinkName { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public int? Order { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}
