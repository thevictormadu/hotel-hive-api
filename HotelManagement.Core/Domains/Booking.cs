namespace HotelManagement.Core.Domains
{
    public class Booking : BaseEntity
    {
        public bool PaymentStatus { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
