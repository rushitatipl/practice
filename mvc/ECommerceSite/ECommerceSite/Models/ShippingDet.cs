using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceSite.Models
{
    public class ShippingDet
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