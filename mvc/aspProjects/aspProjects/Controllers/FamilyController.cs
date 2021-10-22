using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspProjects.Models;
using  System.Data.Entity;

namespace aspProjects.Controllers
{
    public class FamilyController : Controller
    {
        // GET: Family
      
        public ActionResult Index(string Searchby, string search)
        {
            using (DbModel dbmodel = new DbModel())
            {

                //return View(dbmodel.families.ToList());
                return View(dbmodel.families.Where(x=>x.member.Contains(search) || search==null).ToList());
                var usa = dbmodel.families.Where(x => x.member.Equals("nick")).Count();
                ViewBag.usa = usa;
               
            }
        }

        public ActionResult DtLocation()
        {
            // return 1/1/0001 12:00:00 AM  
            DateTime defaultDate = default(DateTime);
            // return 08/05/2016 12:56 PM  
            var shortDT = defaultDate.ToString().Replace("12:00:00 AM", "");
            // return 08/05/2016 12:56 PM     
            var userDt = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            // return 08/05/2016 12:56 PM  
            var nwDt = DateTime.Now.ToShortDateString();
            // return 12:24 PM  
            var nwTm = DateTime.Now.ToShortTimeString();
            // return 8/5/2016 12:00:00 AM  
           
            ViewData["removeTm"] = shortDT;
            ViewData["nowDt"] = nwDt;
            ViewData["nowTm"] = nwTm;
            
           
            return View();
        }

        // GET: Family/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {

            using (DbModel dbmodel =new DbModel())
            {
                return View(dbmodel.families.Where(x => x.memberid == id).FirstOrDefault());
            }
        }

        // GET: Family/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Family/Create
        [HttpPost]
        public ActionResult Create(family fam)
        {
            try
            {
                // TODO: Add insert logic here
                using (DbModel dbmodel =new DbModel())
                {
                    dbmodel.families.Add(fam);
                    dbmodel.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Family/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel dbmodel = new DbModel())
            {
              //  return View(dbmodel.families.Where(x => x.memberid == id).FirstOrDefault());
                return View(dbmodel.families.Where(x => x.memberid == id).FirstOrDefault());
            }
        }

        // POST: Family/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, family fam)
        {
            try
            {
                // TODO: Add update logic here
                //using (DbModel dbmodel = new DbModel())
                //{
                //    dbmodel.Entry(fam).State = EntityState.Modified;
                //    dbmodel.SaveChanges();
                //}
               // return RedirectToAction("Index");


                using (DbModel dbModels = new DbModel())
                {
                    dbModels.Entry(fam).State = EntityState.Modified;
                    dbModels.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Family/Delete/5
        public ActionResult Delete(int id)
        {
            using(DbModel dbmodel = new DbModel())
            {
                return View(dbmodel.families.Where(x => x.memberid == id).FirstOrDefault());
            }
           
        }

        // POST: Family/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DbModel dbmodel = new DbModel())
                {
                 family fam = dbmodel.families.Where(x => x.memberid == id).FirstOrDefault();
                 dbmodel.families.Remove(fam);
                 dbmodel.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
