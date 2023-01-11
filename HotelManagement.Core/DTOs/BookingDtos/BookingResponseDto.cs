using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs.BookingDtos
{
    public class BookingResponseDto
    {
        public bool PaymentStatus { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public string CustomerId { get; set; }
        public string RoomTypeId { get; set; }
        public string PaymentId { get; set; }
    }
}
