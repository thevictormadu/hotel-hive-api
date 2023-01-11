using HotelManagement.Core.Domains;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/Customers/customerId")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsServices _transactionServices;
        private readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;

        public TransactionsController(ITransactionsServices transactionServices, ILogger logger, UserManager<AppUser> userManager)
        {
            _transactionServices = transactionServices;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("Hotel/transactions"), Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Get(string customersId, string hotelId, int pageNumber, int pageSize)
        {
            try
            {

                var result = await _transactionServices.GetAllUserTransactionForAnHotel(customersId, hotelId, pageNumber, pageSize);
                //_logger.LogInformation("Get all transaction by user triggered");
                if (!result.Succeeded) return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Unable to retrieve users transactions for hotel");
                return BadRequest(ex.Message);
            }

        }
    }
}