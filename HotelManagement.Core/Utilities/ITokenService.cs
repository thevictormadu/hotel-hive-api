using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Utilities
{
    public interface ITokenService
    {
        string CreateToken(UserModel user);
        string CreateToken(ManagerRequest request);
        RefreshToken SetRefreshToken();
    }
}
