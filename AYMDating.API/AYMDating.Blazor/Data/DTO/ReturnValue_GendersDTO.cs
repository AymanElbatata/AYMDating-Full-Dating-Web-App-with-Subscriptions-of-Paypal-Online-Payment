using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_GendersDTO
    {
        public string Type { get; set; }
        public List<Gender>? Data { get; set; }

        public ReturnValue_GendersDTO()
        {
                Data = new List<Gender>();
        }

    }
}
