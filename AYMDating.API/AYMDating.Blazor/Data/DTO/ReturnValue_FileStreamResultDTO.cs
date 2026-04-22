using AYMDating.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_FileStreamResultDTO
    {
        public string Type { get; set; }
        public IFormFile? Data { get; set; }
    }
}
