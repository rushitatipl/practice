using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using onlineSite.Models;

namespace onlineSite.Controllers
{
    public class aadminController : Controller
    {

       
        // GET: aadmin
        public ActionResult dashboard()
        {
            return View();
        }

        // GET: aadmin/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.products.Where(a => a.p_id == id).FirstOrDefault());
            }
        }
        
        public ActionResult categoty(string search,string option, int cid)
        {
                DbModel db = new DbModel();
                 if (option == "category_id")
                 {
               
                    return View(db.categories.Where(x => x.category_name == search || search == null).ToList());
                 }
                 else if (option == "category_name")
                {
                    return View(db.categories.Where(x => x.category_name == search || search == null).ToList());
                }
            else
            {
                return View(db.categories.ToList());
            }


        }
        // GET: aadmin/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: aadmin/Create
        [HttpPost]
        public ActionResult CreateCategory(category cat)
        {
            try
            {
                using(DbModel db=new DbModel())
                {
                    db.categories.Add(cat);
                    db.SaveChanges();
                }

                return RedirectToAction("categoty");
            }
            catch
            {
                return View();
            }
        }

        // GET: aadmin/Edit/5
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.categories.Where(x=>x.category_id==id).FirstOrDefault());
            }
        }

        // POST: aadmin/Edit/5
        [HttpPost]
        public ActionResult EditCategory(int id, category cat)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(cat).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("categoty");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult product(string search, string option)
        {
            DbModel db = new DbModel();

            if (option == "p_name")
            {
                return View(db.products.Where(x => x.p_name == search || search == null).ToList());
            }
           
            else
            {
          

                return View(db.products.ToList());


            }

        }

        public ActionResult prod(string id)
        {
            productModel productModel = new productModel();
            ViewBag.products = productModel.findAll();
            return View();
            //List<product> pd = new List<product>();
            //if (Session["cart"] == null)
            //{
            //    List<Item> cart = new List<Item>();
            //    cart.Add(new Item { Product1 = productModel., Quantity = 1 });
            //    Session["cart"] = cart;
            //}
        }

        
        // GET: aadmin/Create
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: aadmin/Create
        [HttpPost]
        public ActionResult CreateProduct(product cat, HttpPostedFileBase file)
        {
            if (file != null)
            {
                DbModel db = new DbModel();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Image/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                //save new record in database
                product newRecord = new product();
                newRecord.quantity =Convert.ToInt32 (Request.Form["quantity"]);
                newRecord.p_status = Request.Form["p_status"];
                newRecord.p_name = Request.Form["p_name"];
                newRecord.p_desc = Request.Form["p_desc"];
                newRecord.product_price = Convert.ToInt32(Request.Form["product_price"]);
                newRecord.category_id = Convert.ToInt32(Request.Form["category_id"]);

                newRecord.p_img = ImageName;
                db.products.Add(newRecord);
                db.SaveChanges();

            }
            //Display records
            return RedirectToAction("product");

            //try
            //{

            //    using (DbModel db = new DbModel())
            //    {
            //        db.products.Add(cat);

            //        //string path = Server.MapPath("~/App-Data/Image");
            //        //string fileName = Path.GetFileName(cat.p_img.FileName);
            //        //string fullpath = Path.Combine(path, fileName);
            //        //cat.p_img.SaveAs(fullpath);

            //        db.SaveChanges();

            //    }

            //    return RedirectToAction("product");
            //}
            //catch
            //{
            //    return View();
            //}

        }

        // 
               
        

        // GET: aadmin/Edit/5
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.products.Where(x => x.p_id == id).FirstOrDefault());
            }
        }

        // POST: aadmin/Edit/5
        [HttpPost]
        public ActionResult EditProduct(int id, product cat)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(cat).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("product");
            }
            catch
            {
                return View();
            }
        }

        // GET: aadmin/Delete/5
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {

            using (DbModel db = new DbModel())
            {
                return View(db.products.Where(a => a.p_id == id).FirstOrDefault());
                //return View(db.customer_reg.Where(x => x.customer_id == id).FirstOrDefault());
            }
        }

        // POST: aadmin/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DbModel db = new DbModel())
                {
                    product pro = db.products.Where(a => a.p_id == id).FirstOrDefault();
                    db.products.Remove(pro);
                    db.SaveChanges();
                    //customer_reg cust = db.customer_reg.Where(x => x.customer_id == id).FirstOrDefault();
                    //db.customer_reg.Remove(cust);
                    //db.SaveChanges();
                }
                return RedirectToAction("product");
            }
            catch
            {
                return View();
            }
        }
    }
}
