using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserPackage : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public int? PackageId { get; set; }
        public DateTime PackageEndDate { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Package? Package { get; set; }
    }
}
