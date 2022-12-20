namespace HotelManagement.Core.Domains
{
    public class Rating : BaseEntity
    {
        public int Ratings { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
