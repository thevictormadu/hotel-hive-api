using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class WishList : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
        public Customer Customer { get; set; }
        public Hotel Hotel { get; set; }
    }
}
