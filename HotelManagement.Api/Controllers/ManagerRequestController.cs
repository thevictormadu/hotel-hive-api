
using HotelManagement.Core;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRequestController : ControllerBase
    {
        private readonly IManagerRequestService _managerRequestService;

        public ManagerRequestController(IManagerRequestService managerRequestService)
        {
            _managerRequestService = managerRequestService;
        }

        [HttpPost("ManagerRequest")]
        public async Task<IActionResult> ManagerRequest(ManagerRequestDTO requestDTO)
        {

            var result = await _managerRequestService.ManagerRequest(requestDTO);
            if (result.Succeeded == false) return BadRequest(result);
            return Ok(result);
             
        }

        [HttpPost("AdminSendInvite/{requestId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminSendInvite(string requestId)
        {
            var result = await _managerRequestService.AdminSendInvite(requestId);
            if (result.Succeeded == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("ManagerAcceptInvite/{token}")]
        public async Task<IActionResult> ManagerAcceptInvite(string token)
        {
            var result = await _managerRequestService.ManagerAcceptInvite(token);
            if (result.Succeeded == false) return BadRequest(result);
            return Ok(result);
        }

    }
}
