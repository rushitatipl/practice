//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlineSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class product
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public int product_price { get; set; }
        public string p_desc { get; set; }
        public string p_img { get; set; }
        public string p_status { get; set; }
        public string category_id { get; set; }
        public byte[] createddate { get; set; }
        public int quantity { get; set; }
        public HttpPostedFileBase ImageFile{ get; set; }
    }
}