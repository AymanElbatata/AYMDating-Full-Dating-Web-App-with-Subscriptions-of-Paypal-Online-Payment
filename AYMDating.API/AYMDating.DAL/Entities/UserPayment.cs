using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class UserPayment : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? PaymentId { get; set; }
        public string? PayerId { get; set; }
        public string? token { get; set; }
        public int? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }

        public virtual AppUser? AppUser { get; set; }

    }
}
