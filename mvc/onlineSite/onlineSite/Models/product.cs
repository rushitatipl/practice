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
    
    public partial class product
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public Nullable<int> product_price { get; set; }
        public string p_desc { get; set; }
        public string p_img { get; set; }
        public string p_status { get; set; }
        public int category_id { get; set; }
        public System.DateTime createddate { get; set; }
        public int quantity { get; set; }
    }
}
