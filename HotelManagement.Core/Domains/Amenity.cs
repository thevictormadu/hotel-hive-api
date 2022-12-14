using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Amenity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt  { get; set; }

    }
}
