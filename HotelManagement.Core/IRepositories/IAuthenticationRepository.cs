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
        Task<Response<object>> Login(LoginDTO model);
        Task<Response<object>> Register(RegisterDTO user);
        Task<Response<object>> RefreshToken();
    }
}
