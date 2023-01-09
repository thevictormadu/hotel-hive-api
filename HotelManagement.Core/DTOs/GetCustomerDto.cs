using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class GetCustomerDto
    {
        public string CreditCard { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public AppUserCustomerDto AppUserCustomerDto { get; set; }
        //public AppUser AppUser { get; set; }
        //public ICollection<Booking> Bookings { get; set; }
        //public ICollection<WishList> WishLists { get; set; }
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<Rating> Ratings { get; set; }
    }
}
