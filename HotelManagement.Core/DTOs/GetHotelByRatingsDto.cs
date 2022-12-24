using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class GetHotelByRatingsDto
    {
        public string Name { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = String.Empty;

        public string City { get; set; } = String.Empty;

        public string State { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; }
    }
}
