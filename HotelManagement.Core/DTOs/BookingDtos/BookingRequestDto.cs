using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs.BookingDtos
{
    public class BookingRequestDto
    {
        /// <summary>
        ///  public string BookingReference { get; set; }
        /// </summary>

        //public DateTime CheckIn { get; set; }
        //Some implementation to be made on the datetime
        // public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public string CustomerId { get; set; }
        public string RoomTypeId { get; set; }
        public string HotelId { get; set; }
    }
}
