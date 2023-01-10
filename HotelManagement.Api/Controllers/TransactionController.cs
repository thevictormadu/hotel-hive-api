using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
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

         
        [HttpGet("GetAllRoomTransactionForManager")]
        public async Task<ActionResult<Response<RoomTransactionDTO>>>GetAllRoomTransactionForManager(string mangerId)
        {
            try
            {
                ~var roomTransactions = await _transactionService.GetRoomTransactionsByManger(mangerId);
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
    }
}
