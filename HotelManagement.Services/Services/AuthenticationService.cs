using HotelManagement.Core;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Identity;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository repository;

        public AuthenticationService(IAuthenticationRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Response<string>> Login(LoginDTO model)
        {
            return await repository.Login(model);
        }

        public async Task<Response<string>> RefreshToken()
        {
            return await repository.RefreshToken();
        }

        public async Task<Response<string>> Register(RegisterDTO user)
        {
            var result = await repository.Register(user);
            var response = new Response<string>();
            if (result)
            {
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Successfully registered";
            }
            else
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                response.Message = "Failed to register, please change check the email, username and password.";
            }

            return response;
        }

        public async Task<object> ChangePassword(ChangePasswordDTO model)
        {
            if (model.ConfirmNewPassword != model.NewPassword) return "Password does not match";
            var response = await repository.ChangePassword(model);
            return response;
        }

        public async Task<object> ResetPasswordAsync(UpdatePasswordDTO model)
        {
            var response = await repository.ResetPassword(model);
            return response;
        }

        public async Task<object> ForgottenPassword(ResetPasswordDTO model)
        {
            var response = await repository.ForgottenPassword(model);
            return response;
        }
    }
}
