using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Utilities
{
    public interface IToken
    {
        object CreateToken(UserModel user);
        RefreshToken SetRefreshToken();
    }
}
