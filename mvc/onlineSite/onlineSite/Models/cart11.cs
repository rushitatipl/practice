using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineSite.Models
{
    public class cart11
    {
        public int product_id { get; set; }
        public string productname { get; set; }
        
        public Nullable<int> quantity { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> bill { get; set; }
    }
}