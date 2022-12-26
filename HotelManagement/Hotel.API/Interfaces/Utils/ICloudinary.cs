namespace Hotel.API.Interfaces.Utils
{
    public interface ICloudinary
    {
        string UploadToCloudinary(IFormFile file);
    }
}
