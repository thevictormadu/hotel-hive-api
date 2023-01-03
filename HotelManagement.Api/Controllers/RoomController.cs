using HotelManagement.Core.IServices;
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
    }
}
