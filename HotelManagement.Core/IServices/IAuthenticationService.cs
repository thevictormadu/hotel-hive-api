using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Login(LoginDTO model);
        Task<Response<string>> Register(RegisterDTO user);
        Task<Response<string>> RefreshToken();
        public Task<object> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPasswordAsync(UpdatePasswordDTO resetPasswordDTO);
        public Task<object> ForgottenPassword(ResetPasswordDTO model);
    }
}
