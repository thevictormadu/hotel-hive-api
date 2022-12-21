using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<APIResponse<object>> Login(LoginDTO model);
        Task<APIResponse<object>> Register(RegisterDTO user);
        Task<APIResponse<object>> RefreshToken();
    }
}
