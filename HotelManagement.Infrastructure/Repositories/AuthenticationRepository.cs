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

using HotelManagement.Core.IServices;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AuthenticationRepository(UserManager<AppUser> userManager, ITokenService token, 
            ITokenDetails tokenDetails, IHttpContextAccessor httpContext,
            RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _token = token;
            _tokenDetails = tokenDetails;
            _httpContext = httpContext;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public string GetId() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<Response<LoginUserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var response = new Response<LoginUserDTO>();
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
                response.Data = new LoginUserDTO
                {
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    username = user.UserName,
                    roles = userRoles,
                    token = _token.CreateToken(UserModel)
                };
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

        public async Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(GetId());
            var response = new Response<string>
            {
                Succeeded = false,
                Data = String.Empty,
                StatusCode = (int)HttpStatusCode.OK,
            };
            if (user == null) { 
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Please login to change password";
                return response;
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded) { 
                response.StatusCode= (int)HttpStatusCode.ExpectationFailed;
                response.Message = "Unable to change password: password should contain a Capital, number, character and minimum length of 8. Confirm if old password is correct.";
                return response;
            }
            response.Message = "Password changed succesffully";
            response.Succeeded = true;
            return response;
        }

        public async Task<Response<string>> ForgottenPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var response = new Response<string> { Succeeded = false };
            if (user == null)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "The Email Provided is not associated with a user account";
                return response;
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailMsg = new EmailMessage(new string[] { user.Email }, "Reset your password", $"Please Follow the Link to reset your Password: http://localhost:3000/reset-update-password?token={resetPasswordToken}&email={user.Email}");
            await _emailService.SendEmailAsync(emailMsg);
            response.Succeeded = true;
            response.Message = "A password reset Link has been sent to your email address";
            response.StatusCode = (int)HttpStatusCode.OK;
            return response;
        }

        public async Task<object> ResetPassword(UpdatePasswordDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return "The Email Provided is not associated with a user account.";
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                return "The Provided Reset Token is Invalid or Has expired";
            }
            return "Password Reset Successfully";
        }

        public async Task Signout()
        {
            var headers = _httpContext.HttpContext.Request.Headers;
            headers.Remove("Authorisation");
        }
    }
}
