using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Domains
{
    public class Manager : BaseEntity
    {
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
