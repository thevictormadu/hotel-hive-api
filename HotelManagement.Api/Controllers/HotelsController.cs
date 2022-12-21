using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        public HotelsController(HotelDbContext context, IHotelRepository hotelRepo) { _hotelRepo = hotelRepo; }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepo.GetHotelsAsync();
            return Ok(hotels);
        }
        [HttpGet("getHotelById")]
        public async Task<IActionResult> GetAnHotelById([FromBody] int Id)
        {
            var hotel = await _hotelRepo.GetHotelByIdAsync(Id);
            if (hotel == null) { return BadRequest("Hotel does not exist"); }
            return Ok(hotel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotelById([FromQuery] UpdateHotelDto updateHotell, int Id)
        {
            var hotel = _hotelRepo.UpdateHotelAsync(Id);
            if (hotel == null) { return BadRequest("Hotel does noe exist"); }
            return Ok("Hotel information updated successfully");
        }
    }
}
