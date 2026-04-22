using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_CountriesDTO
    {
        public string Type { get; set; }
        public List<Country>? Data { get; set; }

        public ReturnValue_CountriesDTO()
        {
                Data = new List<Country>();
        }

    }
}
