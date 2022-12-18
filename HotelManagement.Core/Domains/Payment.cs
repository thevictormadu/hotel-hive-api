using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Payment
    {
        [Key]
        public string BookingId { get; set; }
        public string TransactionReference { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string MethodOfPayment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Booking Booking { get; set; }
    }
}
