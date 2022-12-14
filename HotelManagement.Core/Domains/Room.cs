using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Room
    {
        public int Id { get; set; }

        public string RoomNo { get; set; }

        public bool IsBooked { get; set; }

        public string RoomTypeId { get; set; }
        public RoomType RoomTypes { get; set; }
        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
