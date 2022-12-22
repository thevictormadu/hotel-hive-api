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
        private readonly IHotelServices _hotelRepo;
        public HotelsController(HotelDbContext context, IHotelServices hotelRepo) { _hotelRepo = hotelRepo; }

        [HttpGet("get-all-hotels")]//get-all-hotels
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepo.GetHotelsAsync();
            return Ok(hotels);
        }
        [HttpGet("get-Hotel-By-Id")]//get-hotel-by-Id
        public async Task<IActionResult> GetAnHotelById([FromRoute] int Id)
        {
            var hotel = await _hotelRepo.GetHotelByIdAsync(Id);
            if (hotel == null) { return BadRequest("Hotel does not exist"); }
            return Ok(hotel);
        }

        [HttpPut("update-hotel-by-Id")]//update-hotel-by-Id
        public async Task<IActionResult> UpdateHotelById([FromQuery] UpdateHotelDto updateHotell, int Id)
        {
            var hotel = await _hotelRepo.UpdateHotelAsync(Id);
            if (hotel == null) { return BadRequest("Hotel does noe exist"); }
            return Ok("Hotel information updated successfully");
        }
    }
}
