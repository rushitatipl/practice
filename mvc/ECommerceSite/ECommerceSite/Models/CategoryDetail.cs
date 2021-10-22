using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models
{
    public class CategoryDetail
    {
      
        public int category_id { get; set; }

        [Required(ErrorMessage = "Category Name Required..")]
        public string category_name { get; set; }

        [Required(ErrorMessage = "Category Name Required..")]
        public int parent_category_id { get; set; }
        public Nullable<bool> category_status { get; set; }
    }

    public class ProductDetail
    {
        public int p_id { get; set; }
        [Required(ErrorMessage = "Product Name Required..")]
        public string p_name { get; set; }
        [Range(typeof(int),"1","200000",ErrorMessage ="Invalid Price")]
        public int product_price { get; set; }
        public string p_desc { get; set; }
        public string p_img { get; set; }
        public Nullable<bool> p_status { get; set; }
        public string category_id { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public int quantity { get; set; }
        public SelectList categories { get; set; }
    }
}