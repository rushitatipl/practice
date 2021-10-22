using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace mvcWEF.Models
{
    public class studentModel
    {
       public int id { get; set; }
        [DisplayName("Student Name")]
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }
    }
}