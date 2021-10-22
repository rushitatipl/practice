using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace onlineSite.Models
{
    public class Shipdetail
    {
        public int shippingdetailid { get; set; }
        [Required(ErrorMessage = "Require Field...")]
        public int customer_id { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string city { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string state { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string country { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string zipcode { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public int order_id { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public decimal amountpaid { get; set; }

        [Required(ErrorMessage = "Require Field...")]
        public string paymenttype { get; set; }
    }
}