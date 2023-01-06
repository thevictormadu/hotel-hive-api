using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class GetRoomDto
    {
        public string RoomNo { get; set; } = string.Empty;

        public bool IsBooked { get; set; } = false;

        public string RoomTypeId { get; set; } = string.Empty ;
        public RoomType RoomTypes { get; set; }
        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
