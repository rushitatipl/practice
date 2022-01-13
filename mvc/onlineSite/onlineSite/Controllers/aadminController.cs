using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using onlineSite.Models;
using System.Web.Hosting;


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


      
        public ActionResult prod(string id, string search, string option,product pr)
        {
            if (option == "p_name")
            {
            //    string[] searchstrings = search.Split(' ');
            //    var suggestions = (from a in pr.p_name
            //                       from w in searchstrings
            //                       where a.Contains(w.ToLower())
            //                       select a).Distinct();
            }
         
            return View();
            //productModel productModel = new productModel();
            //ViewBag.products = productModel.findAll();
            //return View();

        }

        
        // GET: aadmin/Create
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: aadmin/Create
        [HttpPost]
        public ActionResult CreateProduct(productModel model, List<HttpPostedFileBase> file)
        {
            try
            {

                if (file != null)
                {
                    string ImageName;

                    product newRecord = new product();
                    DbModel db = new DbModel();

                    foreach (var files in file)
                    {
                        ImageName = System.IO.Path.GetFileName(files.FileName);
                        string physicalPath = HostingEnvironment.MapPath("~/Image/" + ImageName);
                        files.SaveAs(physicalPath);

                        newRecord.p_img += ImageName + ",";
                        Session["img"] = newRecord.p_img;
                        
                      


                    
                    }
                    // save image in folder
                   //  var user = Session["user"].ToString();

                    //save new record in database
                   
                    newRecord.quantity = model.quantity;
                    // newRecord.quantity =Convert.ToInt32 (Request.Form["quantity"]);
                    newRecord.p_status = model.p_status;
                    newRecord.p_name = model.p_name;
                    newRecord.p_desc = model.p_desc;
                   
                    newRecord.product_price = model.product_price;
                    newRecord.category_id = model.category_id;

                    var img = Session["img"].ToString();
                    db.products.Add(newRecord);
                    db.SaveChanges();
                    var latestId = newRecord.p_id;

                    childProduct cproduct = new childProduct();
                    string[] sp = img.Split(',');

                    for (byte i = 1; i < sp.Length; i++)
                    { 
                    
                    cproduct.pname = model.p_name;
                    cproduct.image = sp[i];
                    cproduct.pid = latestId;
                    db.childProducts.Add(cproduct);
                    db.SaveChanges();
                    }
                }
                //Display records
                return RedirectToAction("product");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        // show product
               
        

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
