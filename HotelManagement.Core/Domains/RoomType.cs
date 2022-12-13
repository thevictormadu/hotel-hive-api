using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class RoomType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public string Thumbnail { get; set; }

        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Room> Room { get; set; }
        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
