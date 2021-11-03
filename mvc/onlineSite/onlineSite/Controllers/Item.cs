using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using onlineSite.Models;

namespace onlineSite.Controllers
{
    public class Item
    {
        private product product = new product();

        public product Product1 { get; set; }
        private int quantity;
        public int Quantity { get; set; }
        public Item() { }
        
        public Item(product product,int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}