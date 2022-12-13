using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class WishList
    {
        [Key]
        public int CustomerId { get; set; }

        public int HotelId { get; set; }
        public Customer Customers { get; set; }
        public Hotel Hotel { get; set; }
    }
}
