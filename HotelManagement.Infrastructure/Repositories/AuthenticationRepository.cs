using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Web;

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HotelManagement.Application.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationRepository(UserManager<AppUser> userManager, ITokenService token, 
            ITokenDetails tokenDetails, IHttpContextAccessor httpContext,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _token = token;
            _tokenDetails = tokenDetails;
            _httpContext = httpContext;
            _roleManager = roleManager;
        }

        public async Task<Response<string>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var response = new Response<string>();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var UserModel = new UserModel {
                    Id = user.Id,
                    UserName = model.Username,
                    Role = userRoles.FirstOrDefault() ?? ""
                };
                
                
                var refreshToken = _token.SetRefreshToken();
                //var refreshToken = SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
                response.Succeeded = true;
                response.Data = _token.CreateToken(UserModel);
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Message = "Logged in successfully";
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Wrong Credential";
                
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            return response;
        }
       

        private async Task SaveRefreshToken(AppUser user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Refreshtoken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
        }

        public async Task<Response<string>> RefreshToken()
        {
            var currentToken = _httpContext.HttpContext.Request.Cookies["refresh-token"];
            var user = await _userManager.FindByIdAsync(_tokenDetails.GetId());
             
            var response = new Response<string>();
            response.Succeeded = false;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (user == null || user.RefreshToken != currentToken)
            {
                response.Data = "Invalid refresh token";
            }else if(user.RefreshTokenExpiryTime < DateTime.Now)
            {
                response.Message = "Token Expired";
            }else
            {
                var UserModel = new UserModel
                {
                    Id = _tokenDetails.GetId(),
                    UserName = _tokenDetails.GetUserName(),
                    Role = _tokenDetails.GetRoles()
                };

                response.Succeeded = true;
                response.Data = _token.CreateToken(UserModel);
                response.Message = "Successful refreshed token";
                response.StatusCode = (int)HttpStatusCode.Accepted;

                var refreshToken = _token.SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
            }
            return response;
        }
        public async Task<bool> Register(RegisterDTO user)
        {
            var mapInitializer = new MapInitializer();
            var newUser = mapInitializer.regMapper.Map<RegisterDTO, AppUser>(user);
            
            var result = await _userManager.CreateAsync(newUser, user.Password);
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles.Count == 0) await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
            if (result.Succeeded) await _userManager.AddToRoleAsync(newUser, "Customer");
            
            return result.Succeeded;
        }
    }
}
