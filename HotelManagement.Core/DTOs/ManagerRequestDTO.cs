using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class ManagerRequestDTO
    {
        public string ManagerName { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string Email { get; set; }
    }
}
