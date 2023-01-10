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
        Task<Response<string>> Login(LoginDTO model);
        Task<bool> Register(RegisterDTO user);
        Task<Response<string>> RefreshToken();
    }
}
