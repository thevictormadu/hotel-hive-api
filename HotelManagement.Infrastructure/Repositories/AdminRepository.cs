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

        public async Task<Response<string>> CreateRole(RoleDTO role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = ((Roles)role.RoleName).ToString(),
            };
            var result = await _roleManager.CreateAsync(identityRole);
            var response = new Response<string>();
            if (result.Succeeded)
            {
                response.Succeeded = true;
                response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                response.Message = "Role created successfully";
            }
            else
            {
                response.Succeeded = false;
                response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                response.Message = "Unable to Create Role";
                
            }

            return response;
        }

        public async Task<Response<string>> AddUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new Response<string>
            {
                Succeeded = false,
                StatusCode = (int)System.Net.HttpStatusCode.NotFound,
                Message = "User not found"
            };
            if(user == null) return response;
            
            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            response.Succeeded = true;
            response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            response.Message = "Role added successfully";
            return response;
        }

        public async Task<Response<string>> RemoveUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new Response<string>
            {
                Succeeded = false,
                StatusCode = (int)System.Net.HttpStatusCode.NotFound,
                Message = "User not found"
            };
            if (user == null) return response;

            var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
            response.Succeeded = true;
            response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            response.Message = "Role removed successfully";
            return response;
        }
    }
}
