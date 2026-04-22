using AYMDating.DAL.BaseEntity;

namespace AYMDating.DAL.Entities
{
    public class Education : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public virtual ICollection<UserHistory> UserHistories { get; set; }

    }
}