using AYMDating.API.DTO;
using AYMDating.DAL.Entities;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_UserHistoriesDTO
    {
        public string Type { get; set; }
        public List<UserHistoryDTO>? Data { get; set; }

        public ReturnValue_UserHistoriesDTO()
        {
                Data = new List<UserHistoryDTO>();
        }

    }
}
