using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("Create-role")]
        
        public async Task<IActionResult> CreateRole(RoleDTO role)
        {
            var response = await _adminService.CreateRole(role);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("Add-user-role/{userId}")]
        public async Task<IActionResult> AddUserRole(string userId, Roles roles)
        {
            var response = await _adminService.AddUserRole(userId, roles);
            if(response.Succeeded) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("Remove-user-role/{userId}")]
        public async Task<IActionResult> RemoveUserRole(string userId, Roles roles)
        {
            var response = await _adminService.RemoveUserRole(userId, roles);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }

        //I added this comment
    }
}
