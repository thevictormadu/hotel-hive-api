using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateHotelController : Controller
    {
        private readonly IRateHotelService _rateHotelService;

        public RateHotelController(IRateHotelService rateHotelService)
        {
            _rateHotelService = rateHotelService;
        }

        [HttpPost]
        public async Task<IActionResult> RateHotel([FromBody] RateHotelDTO rateHotelDto)
        {
            if (rateHotelDto == null)
            {
                return BadRequest("Invalid Input");
            }

            var result = await _rateHotelService.RateHotel(rateHotelDto);
            return Ok(result);
        }

        // comment inside RateController
    }
}
