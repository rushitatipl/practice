using System;
using System.Collections.Generic;
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
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpassword { get; set; }
        public string Profile { get; set; }
        public bool IsEmailVerified { get; set; }
        public int Otp { get; set; }
    }
}