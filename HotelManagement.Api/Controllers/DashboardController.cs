using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    public class DashboardController : Controller
    {
        //injected HotelRatingService
        private readonly IHotelRatingService ratingService;
        public DashboardController(IHotelRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> RateHotel(RateHotelDTO request)
        {
            try
            {
                await ratingService.RateHotel(request);
                return Ok("Successful");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
