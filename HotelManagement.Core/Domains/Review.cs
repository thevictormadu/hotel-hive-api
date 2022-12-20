using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Review : BaseEntity
    {
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public string HotelId { get; set; }
        public string CustomerId { get; set; }
        public Hotel Hotel { get; set; }
        public Customer Customer { get; set; } 
    }
}
