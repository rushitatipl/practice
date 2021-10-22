using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loginMVC.Models;

namespace loginMVC.Controllers
{
    public class familyController : Controller
    {
        // GET: family
        public ActionResult Index()
        {
            using(fModel fm=new fModel())
            {
                return View(fm.families.ToList());
            }
          
        }

        // GET: family/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: family/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: family/Create
        [HttpPost]
        public ActionResult Create(family fml)
        {
            try
            {
                using (fModel fm = new fModel())
                {
                    fm.families.Add(fml);
                    fm.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: family/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: family/Edit/5
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

        // GET: family/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: family/Delete/5
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
