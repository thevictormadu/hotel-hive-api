using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        //public string AddressId { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
        [ForeignKey("ManagerId")]
        public Manager? Manager { get; set; }
        public Address? Address { get; set; }
        //public int ManagerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
