
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Domains
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public  string Password { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public int Age{ get; set; }

        public bool IsActive { get; set; }

        public string Publicid { get; set; }

        public  string Avatar { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }




    }
}
