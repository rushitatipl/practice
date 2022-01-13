using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace onlineSite.Models
{
    public class productModel
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public Nullable<int> product_price { get; set; }
        public string p_desc { get; set; }
        public string p_img { get; set; }
        public string p_status { get; set; }
        public int category_id { get; set; }
        public byte[] createddate { get; set; }
        public int quantity { get; set; }

        //child product data
        public int id { get; set; }
        public string pname { get; set; }
        public string image { get; set; }
        public int pid { get; set; }
    }
}