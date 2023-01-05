using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IAuthService
    {
        Task<object> Login(LoginDTO model);
        Task<object> Register(RegisterDTO user);
    }
}
