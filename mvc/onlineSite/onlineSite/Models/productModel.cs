using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace onlineSite.Models
{
    public class productModel
    {
        //List<product> pd = new List<product>();
        //public List<product> pro { get; set; }

        private List<product> products;

        public productModel()
        {
            this.products = new List<product>() {
                new product {
                    p_id = 1,
                    p_name = "Name 1",
                    product_price=10,
                    p_desc="lkj",
                    p_status="1",
                    category_id=2,
                    p_img="asd",
                    quantity=1

                  
                },
                new product {
                     p_id = 2,
                    p_name = "Name 1",
                    product_price=10,
                    p_desc="lkj",
                    p_status="1",
                    category_id=1,
                    p_img="asd",
                    quantity=1

                },
                new product {
                     p_id = 3,
                    p_name = "Name 1",
                    product_price=10,
                    p_desc="lkj",
                    p_status="1",
                    category_id=1,
                    p_img="asd",
                    quantity=1

                }
            };
        }

        public List<product> findAll()
        {
            return this.products;
        }

        public product find(string id)
        {
            return this.products.First(p => p.p_id.Equals(id));
        }

    }
}