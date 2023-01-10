using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class AddRommDto
    {
        public string RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
