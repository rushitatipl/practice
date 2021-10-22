using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loginMVC.Models;

namespace loginMVC.Controllers
{
    public class tiplLogController : Controller
    {
        // GET: tiplLog
        public ActionResult Index()
        {
            using(DbModell db=new DbModell())
            {
                return View(db.tiplLogins.ToList());
            }
           
        }

        // GET: tiplLog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: tiplLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tiplLog/Create
        [HttpPost]
        public ActionResult Create(tiplLogin tipl)
        {
            try
            {

                using (DbModell db = new DbModell())
                {
                    db.tiplLogins.Add(tipl);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tiplLog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tiplLog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tiplLog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tiplLog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
