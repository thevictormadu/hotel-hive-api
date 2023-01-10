using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace HotelManagement.Services.Services
{

    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary _cloudinary;
        private readonly HotelDbContext context;

        public CloudinaryService(Cloudinary cloudinary, HotelDbContext context)
        {
            _cloudinary = cloudinary;
            this.context = context;
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

        public async Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images)
        {

            if (images == null || images.Length == 0)
            {
                return "no image";
            }

           
                var user = await context.Customers.Include(cust => cust.AppUser).Where(x => x.AppUser.Id == userId).FirstOrDefaultAsync();

            if (user == null)
                {
                    return "Couldnt find user";
                }
                await _cloudinary.DeleteResourcesAsync(user.AppUser.Avatar);

                string avatar = "";
                foreach (var image in images)
                {
                    var result = await _cloudinary.UploadAsync(new ImageUploadParams
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream())
                    }).ConfigureAwait(false);
                    avatar += result.Url;
                }
                if (avatar.Length == 0) return "Failed to upload";

                user.AppUser.Avatar = avatar;
                context.Customers.Update(user);
                await context.SaveChangesAsync();


                return "Done";

            }

        public async Task<string> UpdateManagerPhotosAsync(string userId, IFormFile[] images)
        {

            if (images == null || images.Length == 0)
            {
                return "no image";
            }



            var user = await context.Managers.Include(man => man.AppUser).Where(x => x.AppUser.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return "Couldnt find user";
            }
            await _cloudinary.DeleteResourcesAsync(user.AppUser.Avatar);

            string avatar = "";
            foreach (var image in images)
            {
                var result = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                }).ConfigureAwait(false);
                avatar += result.Url;
            }
            if (avatar.Length == 0) return "Failed to upload";

            user.AppUser.Avatar = avatar;
            context.Managers.Update(user);
            await context.SaveChangesAsync();


            return "Done";

        }


    }
}
