
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Core.IServices
{
    public interface ICloudinaryService
    {
        Task<List<Dictionary<string, string>>> UploadAsync(IFormFile[] images);
        Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images);
        Task<string> UpdateManagerPhotosAsync(string userId, IFormFile[] images);


    }
}
