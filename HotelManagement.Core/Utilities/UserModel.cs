using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Utilities
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } = "";
    }
}
