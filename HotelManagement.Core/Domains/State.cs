using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class State : BaseEntity
    {

        public string Name { get; set; }

        public string code { get; set; }

        public Address Address { get; set; }
    }
}
