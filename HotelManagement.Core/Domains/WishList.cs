using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class WishList : BaseEntity
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
