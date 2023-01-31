using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs.ReviewDTOs
{
    public class AddReviewToReturnDTOs
    {
        public string Comment { get; set; }
        public string HotelId { get; set; }
        public string CustomerId { get; set; }
        public string CreatedAt { get; set; }
    }
}
