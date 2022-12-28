using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;

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

    }
}
