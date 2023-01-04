using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace HotelManagement.Services.Services
{

    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<List<Dictionary<string, string>>> UploadAsync(IFormFile[] images)
        {
            var results = new List<Dictionary<string, string>>();

            if (images == null || images.Length == 0)
            {
                return null;
            }

            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            foreach (var image in images)
            {
                if (image.Length == 0) return null;

                var result = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName,
                        image.OpenReadStream()),
                }).ConfigureAwait(false);

                var imageProperties = new Dictionary<string, string>();
                foreach (var token in result.JsonObj.Children())
                {
                    if (token is JProperty prop)
                    {
                        imageProperties.Add(prop.Name, prop.Value.ToString());
                    }
                }

                results.Add(imageProperties);

            }

            return results;
        }

    }
}
