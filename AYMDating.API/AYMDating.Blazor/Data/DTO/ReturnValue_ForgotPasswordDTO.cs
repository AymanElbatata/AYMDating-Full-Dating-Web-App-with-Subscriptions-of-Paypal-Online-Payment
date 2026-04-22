using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_ForgotPasswordDTO
    {
        public string Type { get; set; }
        public ForgotPasswordDTO? Data { get; set; }
    }
}
