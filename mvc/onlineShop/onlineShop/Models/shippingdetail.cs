//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlineShop.shop
{
    using System;
    using System.Collections.Generic;
    
    public partial class shippingdetail
    {
        public int shippingdetailid { get; set; }
        public int customer_id { get; set; }
        public string Adress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public int order_id { get; set; }
        public decimal amountpaid { get; set; }
        public string paymenttype { get; set; }
    }
}
