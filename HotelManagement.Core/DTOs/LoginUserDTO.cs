using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class LoginUserDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public IList<string> roles { get; set; }
        public string token { get; set; }
    }
}
