using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {

            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetHotels();
            return Ok(hotels);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] string Id)
        {
            var hotel = await _hotelService.GetHotelById(Id);
            return Ok(hotel);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateById(UpdateHotelDto update, string Id)
        {
            var hotel = await _hotelService.UpdateHotel(update, Id);
            return Ok(hotel);
        }
        [HttpGet("Rooms-By-Availability")]
        public async Task<IActionResult> GetRoomsByAvailability(string HotelName,string RoomType)
        {
            var result = await _hotelService.GetRoomsByAvailability(HotelName,RoomType);
            if(!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("Available-Rooms-By-Id")]
        public async Task<IActionResult> RoomsAvailableById(string HotelName, string roomId)
        {
            var result = await _hotelService.GetAvailableRoomsBy(HotelName, roomId);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("Ratings")]
        public async Task<IActionResult> GetHotelRatings(string HotelName)
        {
            var result = await _hotelService.GetHotelRating(HotelName);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("add-hotel")]
        public async Task<IActionResult> AddHotel (string Manager_Id, [FromBody] AddHotelDto addHotelDto)
        {
            if (addHotelDto== null)
            {
                return BadRequest("Invalid Input");
            }
            var result =await _hotelService.AddHotel(Manager_Id, addHotelDto);
            return Ok(result);

        }


        [HttpDelete ("Id")]
        public async Task<IActionResult> DeleteHotelById(string id)
        {
            var result = await _hotelService.DeleteHotelById(id);
            if(!result.Succeeded) return BadRequest (result);
            return Ok(result);
        }
        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchHotel([FromRoute] string Id, [FromBody] UpdateHotelDto update)
        {
            // Call the UpdateHotel method of the HotelService
            var result = await _hotelService.UpdateHotel(update, Id);

            // If the update was successful, return an OK response with the updated hotel data
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            // Otherwise, return a Bad Request response with the error message
            return BadRequest(result.Message);
        }
        [HttpGet("By-State")]
        public async Task<IActionResult> GetHotelByState(string State)
        {
            var result = await _hotelService.GetHotelByState(State);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }


    }

}
