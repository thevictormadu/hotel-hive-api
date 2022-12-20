namespace HotelManagement.Core.Domains
{
    public class Amenity : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }

    }
}
