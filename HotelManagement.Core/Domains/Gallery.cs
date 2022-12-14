using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class Gallery
    {
        public Guid Id { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeature { get; set; }

        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
