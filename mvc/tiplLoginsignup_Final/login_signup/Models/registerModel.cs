using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace login_signup.Models
{
    public class registerModel
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string Cpassword { get; set; }
        public string Profile { get; set; }
        public bool IsEmailVerified { get; set; }
        public int Otp { get; set; }
    }
}