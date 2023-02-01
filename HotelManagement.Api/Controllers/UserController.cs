using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ICloudinaryService _cloudinaryService;

        public UserController(ICloudinaryService cloudinaryService)
        {
            //Added the cloudinary service
            _cloudinaryService = cloudinaryService;
        }

        //[Authorize]
        [HttpPut("{userId}/customerPhotos")]
        public async Task<IActionResult> UpdateUserPhotosAsync(string userId, IFormFile[] images)
        {
            var result = await _cloudinaryService.UpdateUserPhotosAsync(userId, images);
            return Ok(result);
        }

        [HttpPut("{userId}/managerPhotos")]
        public async Task<IActionResult> UpdateManagerPhotosAsync(string userId, IFormFile[] images)
        {
            var result = await _cloudinaryService.UpdateManagerPhotosAsync(userId, images);
            return Ok(result);
        }

    }
}
