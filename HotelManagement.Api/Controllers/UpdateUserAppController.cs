using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateUserAppController : ControllerBase
    {
        private readonly IUpdateUserAppService _userService;

        public UpdateUserAppController(IUpdateUserAppService userService)
        {
            _userService = userService;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateById(UpdateAppUserDto update, string Id)
        {
            var user = await _userService.UpdateUser(update, Id);
            return Ok(user);
        }
    }
}
