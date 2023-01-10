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
        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom(string RoomType_ID, string Hotel_Name,[FromBody] AddRoomDto addRoomDto)
        {
            if (addRoomDto == null) return BadRequest("Room Not Created");
           var response=await _roomService.AddRoom(RoomType_ID, Hotel_Name, addRoomDto);
            return Ok(response);
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
