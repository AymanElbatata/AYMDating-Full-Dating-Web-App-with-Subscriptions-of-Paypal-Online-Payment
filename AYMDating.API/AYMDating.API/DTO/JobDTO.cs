using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Entities;

namespace AYMDating.API.DTO
{
    public class JobDTO : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}
