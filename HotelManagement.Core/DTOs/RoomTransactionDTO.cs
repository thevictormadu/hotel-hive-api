using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class RoomTransactionDTO
    {
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string BookingReference { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }




    }
}
