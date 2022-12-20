using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string? BankCode { get; set; }
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
    }
}
