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
using System.Net.Http;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToken _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(UserManager<AppUser> userManager,IConfiguration configuration,IToken token,ITokenDetails tokenDetails, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _token = token;
            _tokenDetails = tokenDetails;
            _httpContext = httpContext;
            _configuration = configuration;
        }

        public async Task<APIResponse<object>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var response = new APIResponse<object>();
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
                response.IsSuccess = true;
                response.Result = _token.CreateToken(UserModel);
                response.StatusCode = System.Net.HttpStatusCode.Accepted;
            }
            else
            {
                response.IsSuccess = false;
                response.Result = "Wrong Credential";
                
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            }
            return response;
        }
       

        private async Task SaveRefreshToken(AppUser user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Refreshtoken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
        }

        public async Task<APIResponse<object>> RefreshToken()
        {
            var currentToken = _httpContext.HttpContext.Request.Cookies["refresh-token"];
            var user = await _userManager.FindByIdAsync(_tokenDetails.GetId());
             
            var response = new APIResponse<object>();
            response.IsSuccess = false;
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            if (user == null)
            {
                response.Result = "Invalid token to refresh";
            }else if(user.RefreshToken.ToString() != currentToken || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                response.Result = "Token Expired";
            }else
            {
                var UserModel = new UserModel
                {
                    Id = _tokenDetails.GetId(),
                    UserName = _tokenDetails.GetUserName(),
                    Role = _tokenDetails.GetRoles()
                };

                response.IsSuccess = true;
                response.Result = _token.CreateToken(UserModel);
                response.StatusCode = System.Net.HttpStatusCode.Accepted;

                var refreshToken = _token.SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
            }
            return response;
        }
        public async Task<APIResponse<object>> Register(RegisterDTO user)
        {
            AppUser newAppUser = new AppUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = false,
                Age = user.Age,
                Avatar = "www.xyz.com",
                Gender = user.Gender,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            var result = await _userManager.CreateAsync(newAppUser, user.Password);
            var response = new APIResponse<object>();
            if (result.Succeeded) { 
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.Result = "Successfully registered";
            } else
            {
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.Result = "Failed to register";
                response.ErrorMessages = result.Errors.Select(e => e.Description).ToList();
            }
            return response;
        }
    }
}
