using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Manager
    {
        [Key]
        public string AppUserId { get; set; }
        public string CompanyName { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string State { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
