using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class RoomType : BaseEntity
    {
        public string HotelId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Thumbnail { get; set; }
        public Hotel Hotel { get; set; }
        public int Available { get; set; }
    }
}
