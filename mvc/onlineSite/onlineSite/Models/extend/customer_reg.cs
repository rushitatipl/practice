using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace onlineSite.Models
{
    [MetadataType(typeof(customer_regMetadata))]
    public partial class customer_reg
    {
    }

    public  class customer_regMetadata
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="require field...")]
        public string fname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        public string lname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        [DataType(DataType.PhoneNumber)]
        public string mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="confirm password and password does not match")]
        public string cpassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        public string profile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        public string country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        public string state { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "require field...")]
        public string city { get; set; }


    }

}