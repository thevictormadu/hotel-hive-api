using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Customer
    {
      
        public Guid Id { get; set; }
        public string CreditCard { get; set; }

        public Guid AddressId { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public User User { get; set; }
        
    }
}
