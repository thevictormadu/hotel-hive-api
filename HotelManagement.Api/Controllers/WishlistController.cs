using HotelManagement.Application.Utility;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        [HttpGet("Get-Customer-Wishlist")]
        public async Task<IActionResult> GetCustomerWishList([FromQuery][Required] string customerId, int pageNumber, int pageSize)
        {
            try
            {
                var response = await _wishlistService.GetWishListAsync(customerId, pageNumber, pageSize) ;
                if (response == null)
                {
                    return BadRequest(response);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
