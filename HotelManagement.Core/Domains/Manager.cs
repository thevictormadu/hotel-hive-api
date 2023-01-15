using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Core.Domains
{
    public class Manager : BaseEntity
    {
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string AppUserId { get; set; }       
        public AppUser AppUser { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
