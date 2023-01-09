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
        public string CustomerName { get; set; }
        public string RoomType { get; set; }
        public string Hotel { get; set; }
    }
}
