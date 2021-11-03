using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace onlineSite.Models
{
    public class ResetPasswordModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string cPassword { get; set; }

        public string ResetCode { get; set; }
    }
}