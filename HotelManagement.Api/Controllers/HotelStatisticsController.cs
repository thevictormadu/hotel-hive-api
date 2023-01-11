using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelStatisticsController : ControllerBase
    {
        private readonly IHotelStatisticsService _hotelStatisticsService;

        public HotelStatisticsController(IHotelStatisticsService hotelStatisticsService)
        {
            _hotelStatisticsService = hotelStatisticsService;
        }

        [HttpGet("total-hotels")]
        public async Task<IActionResult> GetTotalHotels()
        {
            var total = await _hotelStatisticsService.GetTotalNumberOfHotels();

            return Ok(total);


        }


        [HttpGet("total-rooms")]
        public async Task<IActionResult> GetTotalRooms([FromQuery] string id)
        {
            var total = await _hotelStatisticsService.GetTotalNumberOfRooms(id);

            return Ok(total);
        }


        [HttpGet("total-rooms-occupied")]
        public async Task<IActionResult> GetTotalRoomsOccupied([FromQuery] string id)
        {
            var total = await _hotelStatisticsService.GetTotalNumberOfRoomsOccupied(id);

            return Ok(total);
        }


        [HttpGet("total-rooms-unoccupied")]
        public async Task<IActionResult> GetTotalRoomsUnoccupied([FromQuery] string id)
        {
            var total = await _hotelStatisticsService.GetTotalNumberOfRoomsUnoccupied(id);

            return Ok(total);
        }
    }
}
