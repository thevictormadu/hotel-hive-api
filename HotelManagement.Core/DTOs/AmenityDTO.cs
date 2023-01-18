using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class AmenityDTO
    {
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        //public string HotelId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public Hotel Hotel { get; set; }
    }
}
