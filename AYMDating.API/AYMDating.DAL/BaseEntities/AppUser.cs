using AYMDating.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.BaseEntity
{
    public class AppUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public bool Activated { get; set; } = false;
        public bool IsStopped { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        //public bool IsSwitchedOff { get; set; } = false;
        public DateTime DateOfJoin { get; set; } = DateTime.Now;

        //public virtual ICollection<UserHistory> UserHistories { get; set; }
        //public virtual ICollection<UserImage> UserImages { get; set; }
        //public virtual ICollection<UserMessage> UserMessages { get; set; }
        //public virtual ICollection<UserMessageGroup> UserMessageGroups { get; set; }
    }
}
