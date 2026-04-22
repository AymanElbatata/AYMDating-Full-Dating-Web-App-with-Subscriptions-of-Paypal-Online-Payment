using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_ActivateUserDTO
    {
        public string Type { get; set; }
        public ActivateUserDTO? Data { get; set; }
    }
}
