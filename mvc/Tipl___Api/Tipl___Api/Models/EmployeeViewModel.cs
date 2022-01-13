using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tipl___Api.Models
{
    public class EmployeeViewModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone_no { get; set; }
        public string position { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<int> salary { get; set; }

        // Test 
    }
}