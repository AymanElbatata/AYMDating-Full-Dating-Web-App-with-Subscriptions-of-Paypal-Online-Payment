using AYMDating.API.DTO;

namespace AYMDating.Blazor.Data.DTO
{
    public class ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO
    {
        public string Type { get; set; }
        public List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>? Data { get; set; }

        public ReturnValue_UserViewOrLikeOrFavoriteOrBlockOrReportListDTO()
        {
                Data = new List<UserViewOrLikeOrFavoriteOrBlockOrReportDTO>();
        }

    }
}
