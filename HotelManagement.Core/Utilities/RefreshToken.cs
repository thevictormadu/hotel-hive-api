using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Utilities
{
    public class RefreshToken
    {
        public string Refreshtoken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
