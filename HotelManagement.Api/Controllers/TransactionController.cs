using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
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
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService) 
        {
            _transactionService = transactionService; 
        }
        //Display all transaction for admin controller
        [HttpGet("DisplayAllTransactionforAdmin")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DisplayAllTransactionToAdmin()
        {
            try
            {
                var result = await _transactionService.DisplayAllTransactionToAdmin();
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAllRoomTransactionForManager")]
        public async Task<ActionResult<Response<RoomTransactionDTO>>>GetAllRoomTransactionForManager(string mangerId)
        {
            try
            {
                var roomTransactions = await _transactionService.GetRoomTransactionsByManger(mangerId);
                if (roomTransactions == null)
                {
                    return BadRequest();
                }
                return Ok(roomTransactions);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        
        }

        [HttpGet("GetAllRoomTransaction")]
        public async Task<ActionResult<Response<RoomTransactionDTO>>> GetAllRoomTransaction(string hotelId)
        {
            try
            {
                var roomTransactions = await _transactionService.GetAllRoomsTransactions(hotelId);
                if (roomTransactions == null)
                {
                    return BadRequest();
                }
                return Ok(roomTransactions);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{customerId}/hotel/{hotelId}/transactions"), Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Get(string customerId, string hotelId, int pageNumber, int pageSize)
        {
            try
            {

                var result = await _transactionService.GetAllCustomerTransactionForAnHotel(customerId, hotelId, pageNumber, pageSize);
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


