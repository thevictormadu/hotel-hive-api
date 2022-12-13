using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Booking
    {
        public Guid  Id { get; set; }

        public string BookingReference { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int  NoOfPeople { get; set; }

        public string ServiceName { get; set; }

        public string  HotelId { get; set; }

        public string  CustomerId { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public Customer Customers { get; set; }
        public Hotel Hotel { get; set; }

        public Payment payment { get; set; }
    }
}
