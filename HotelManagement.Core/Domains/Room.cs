using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Room : BaseEntity
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
        public RoomType Roomtype { get; set; }
        ////Added Hotels
        public string HotelId { get; set; }
        ////Added
        public Hotel Hotel { get; set; }
    }
}
