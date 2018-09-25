using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangeAPI.Dto
{
    public class LiffTest
    {
        public string user_id { get; set; }
    }

    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters")]
        public string password { get; set; }
    }

    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}