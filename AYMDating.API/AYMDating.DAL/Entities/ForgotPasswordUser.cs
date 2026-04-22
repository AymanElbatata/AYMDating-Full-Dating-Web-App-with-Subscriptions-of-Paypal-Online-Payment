using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class ForgotPasswordUser : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? Code { get; set; } = null!;
        public string? Token { get; set; } = null!;
        public DateTime? DateOfExpiration { get; set; }
        public bool IsConfirmed { get; set; } = false;

        public virtual AppUser? AppUser { get; set; }

    }
}
