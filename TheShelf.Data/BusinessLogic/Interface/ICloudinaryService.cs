using Microsoft.AspNetCore.Http;

namespace TheShelf.Service.Interface
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
