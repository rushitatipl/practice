using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace onlineSite.Models
{
    public class categotyDetail
    {
        public int category_id { get; set; }
        [Required(ErrorMessage = "Name is Required...")]
        public string category_name { get; set; }
        public int parent_category_id { get; set; }
        public string category_status { get; set; }
    }
    public partial class productdetail
    {
        public int p_id { get; set; }
        [Required(ErrorMessage = "Name is Required...")]
        public string p_name { get; set; }
        [Required]
        [Range(typeof(int), "1", "300000", ErrorMessage = "Invalid price...")]
        public int product_price { get; set; }
        [Required(ErrorMessage = "Description is Required...")]
        public string p_desc { get; set; }
        public string p_img { get; set; }
        public Nullable<bool> p_status { get; set; }
        public string category_id { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid quantity...")]
        public int quantity { get; set; }
        public SelectList categories { get; set; }
    }
}