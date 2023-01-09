using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoomById(String Id)
        {
            var result = await _roomService.GetRoombyId(Id);
            if(!result.Succeeded) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddRommDto roomDto)
        {
            var result = await _roomService.Create(roomDto);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
    }
}
