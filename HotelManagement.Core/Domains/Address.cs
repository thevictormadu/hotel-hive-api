using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Address :BaseEntity 
    {
        public string  City { get; set; }

        public string Country { get; set; }

        public string Longitude { get; set; }

        public  string Latitude { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
