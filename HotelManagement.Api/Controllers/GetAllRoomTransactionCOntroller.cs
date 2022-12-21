using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllRoomTransactionCOntroller : ControllerBase
    {
        private ITransactionReferenceService _transactionReferenceService;

        public GetAllRoomTransactionCOntroller(ITransactionReferenceService transactionReferenceService) 
        {
            _transactionReferenceService = transactionReferenceService;
        }

         
        [HttpGet]
        public async Task <IActionResult> GetAllRoomTransaction (TransactionReferenceDTO referenceDTO)
        {
            var roomTransactions = await _transactionReferenceService.GetAllRoomTransaction(referenceDTO);
            return Ok (roomTransactions);

        }
    }
}
