
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Core.IServices
{
    public interface ICloudinaryService
    {
        Task<List<Dictionary<string, string>>> UploadAsync(IFormFile[] images);
    }
}
