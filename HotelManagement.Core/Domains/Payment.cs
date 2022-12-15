using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Payment
    {
        [Key]
        public Guid BookingId{ get; set; }
        public Booking Bookings { get; set; }
        public string Transaction { get; set; }
        public double Amount { get; set; }
        public string Status  { get; set; }
        public string MethodOfPayment { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
