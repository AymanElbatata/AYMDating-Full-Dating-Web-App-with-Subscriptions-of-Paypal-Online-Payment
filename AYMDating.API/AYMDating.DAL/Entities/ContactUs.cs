using AYMDating.DAL.BaseEntity;

namespace AYMDating.DAL.Entities
{
    public class ContactUs : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
       
    }
}