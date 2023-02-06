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
        Task<Response<LoginUserDTO>> Login(LoginDTO model);
        Task<Response<string>> Register(RegisterDTO user);
        Task<Response<string>> RefreshToken();
        public Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPasswordAsync(UpdatePasswordDTO resetPasswordDTO);
        public Task<Response<string>> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
