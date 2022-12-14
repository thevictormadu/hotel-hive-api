using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Rating
    {
        public Guid  Id { get; set; }
        public int Ratings { get; set; }

        public int HotelId { get; set; }
        public Customer Customers { get; set; }
        public Hotel Hotel { get; set; }

        public DateTime CreateAt { get; set; }  

        public DateTime Updated { get; set; }
    }
}
