using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class RegisterDTO
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public bool IsActive { get; set; } = false;

        public string Publicid { get; set; }

        public string Avatar { get; set; }

    }
}
