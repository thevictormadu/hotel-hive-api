using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "AppUser Name is required")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
