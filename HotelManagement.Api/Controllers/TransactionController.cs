using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;


        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllTransactionForAdmin()
        {
           
      var result = await _transactionService.DisplayAllTransactionToAdmin();

            if (!result.Succeeded) return BadRequest($"unable to get transactions{result}");
            return Ok(result);


        }


    }
}
