using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class Package : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<UserPackage> UserPackages { get; set; }
    }
}
