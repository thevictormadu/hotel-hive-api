using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var register = await _authenticationService.Register(user);
            if(register.Succeeded == true) return Ok(register);
            return BadRequest(register);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var login = await _authenticationService.Login(model);
            if (login.Succeeded == false) return Unauthorized(login);
            return Ok(login);
        }
        [Authorize]
        [HttpGet("Refresh-Token")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = await _authenticationService.RefreshToken();
            if(token.Succeeded == false) return BadRequest(token);
            return Ok(token);
        }
    }
}
