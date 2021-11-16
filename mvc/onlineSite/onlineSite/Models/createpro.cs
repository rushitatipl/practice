using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineSite.Models
{
    public class createpro
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public int product_price { get; set; }
        public string p_desc { get; set; }
        public HttpPostedFileBase p_img { get; set; }
        public string p_status { get; set; }
        public string category_id { get; set; }
        public byte[] createddate { get; set; }
        public int quantity { get; set; }

        //public HttpPostedFileBase File { get; set; }
    }
}