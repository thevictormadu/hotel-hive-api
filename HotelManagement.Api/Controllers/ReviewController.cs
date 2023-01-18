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


        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewsDTO model, string id)
        {
            var _review = await _reviewService.AddReviewAsync(model, id);

            if (!_review.Succeeded) return BadRequest();
            return Ok(_review);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelReviews(string id)
        {
            var reviews = await _reviewService.GetHotelReviews(id);
            return Ok(reviews);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(string Id , UpdateReviewDto updateReview)
        {
            var review = await _reviewService.UpdateReview(Id, updateReview);
            return Ok(review);
        }
    }
}
