using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
    {
        public string e_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone_no { get; set; }
        public string position { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string filename { get; set; }

        public string email { get; set; }
        public int password { get; set; }
        public string filepath { get; set; }
    }
}