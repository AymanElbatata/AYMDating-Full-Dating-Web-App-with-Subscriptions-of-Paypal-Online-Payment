using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_UserMessagesDTO
    {
        public string Type { get; set; }
        public List<UserMessageDTO>? Data { get; set; }

        public ReturnValue_UserMessagesDTO()
        {
                Data = new List<UserMessageDTO>();
        }

    }
}
