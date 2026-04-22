using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_ListStringsDTO
    {
        public string Type { get; set; }
        public List<string>? Data { get; set; }

        public ReturnValue_ListStringsDTO()
        {
                Data = new List<string>();
        }

    }
}
