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
            return View();
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
                //var res = (from c in db.products
                //           where c.IsDelete == true
                //           select c);

                //if (res != true)
                //{
                  return View(db.products.ToList());
                //}


            }

        }
        // GET: aadmin/Create
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: aadmin/Create
        [HttpPost]
        public ActionResult CreateProduct(product cat)
        {
            //string filename=Path.GetFileNameWithoutExtension(cat)
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.products.Add(cat);
                    db.SaveChanges();
                }

                return RedirectToAction("product");
            }
            catch
            {
                return View();
            }

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
