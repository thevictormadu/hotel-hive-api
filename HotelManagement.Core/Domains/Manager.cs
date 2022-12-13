using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Manager
    {
        [Key]
        public int UserId { get; set; }

        public string CompanyName { get; set; }
        public string BusinessEmail { get; set; }

        public string BusinessPhone { get; set; }

        public string CompanyAddress { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }
        public User Users { get; set; }
        public ICollection<Hotel> Hotel { get; set; }
    }
}
