using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Customer
    {
        
        public string CreditCard { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        [Key]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }

    }
}
