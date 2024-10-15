namespace AesEncription.DTO
{
    public class FileUploadDTO
    {
        public IFormFile File { get; set; }
        public string Key { get; set; } = null!;
    }
}
