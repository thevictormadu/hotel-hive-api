using HotelManagement.Core.DTOs;
using HotelManagement.Core.DTOs.ReviewDTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(string Id , UpdateReviewDto updateReview)
        {
            var review = await _reviewService.UpdateReview(Id, updateReview);
            return Ok(review);
        }
    }
}
