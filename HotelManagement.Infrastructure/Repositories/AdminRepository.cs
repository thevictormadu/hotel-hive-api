using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
using HotelManagement.Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRepository(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<APIResponse<string>> CreateRole(RoleDTO role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = ((Roles)role.RoleName).ToString(),
            };
            var result = await _roleManager.CreateAsync(identityRole);
            var response = new APIResponse<string>();
            if (result.Succeeded)
            {
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = "Role created successfully";
            }
            else
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Result = "Unable to Create Role";
                response.ErrorMessages = response.ErrorMessages.Select(x => x.ToString()).ToList();
            }

            return response;
        }

        public async Task<APIResponse<string>> AddUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new APIResponse<string>
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Result = "User not found"
            };
            if(user == null) return response;
            
            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Result = "Role added successfully";
            return response;
        }

        public async Task<APIResponse<string>> RemoveUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new APIResponse<string>
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Result = "User not found"
            };
            if (user == null) return response;

            var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Result = "Role removed successfully";
            return response;
        }
    }
}
