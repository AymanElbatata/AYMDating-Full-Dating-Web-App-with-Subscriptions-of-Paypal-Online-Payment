using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_UserHistoryDTO
    {
        public string Type { get; set; }
        public UserHistoryDTO? Data { get; set; }

    }
}
