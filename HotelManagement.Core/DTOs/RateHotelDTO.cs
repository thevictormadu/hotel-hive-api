using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class RateHotelDTO
    {
        public int Rating { get; set; }
        public string CustomerId { get; set; }
        public string HotelId { get; set; }

    }
}
