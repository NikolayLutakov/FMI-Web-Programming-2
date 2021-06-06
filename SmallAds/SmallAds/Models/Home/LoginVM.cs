using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmallAds.Models.Home
{
    public class LoginVM
    {
        [Required(ErrorMessage ="*Username can't be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Password can't be empty")]
        public string Password { get; set; }
    }
}
