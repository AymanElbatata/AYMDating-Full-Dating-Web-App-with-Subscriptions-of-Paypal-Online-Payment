using AYMDating.DAL.BaseEntity;

namespace AYMDating.DAL.Entities
{
    public class Country : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        //public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}