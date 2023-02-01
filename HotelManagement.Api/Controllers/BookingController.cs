using HotelManagement.Core.DTOs.BookingDtos;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> Book(BookingRequestDto bookingRequestDto)
        {
            var result = await _bookingService.CreateHotelBooking(bookingRequestDto);
            if (!result.Succeeded) return BadRequest();
            return Ok(result);
        }
        [HttpGet("Per-Manager")]
        public async Task<IActionResult> BookingPerManager(string managerId)
        {
            var result = await _bookingService.GetBookingPerManager(managerId);
            //if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
    }
}
