namespace AYMDating.API.DTO
{
    public class UserImagesUploadDTO
    {
        public string? LinkName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool ToUpload { get; set; } = false;
        public bool ToDelete { get; set; } = false;
    }
}
