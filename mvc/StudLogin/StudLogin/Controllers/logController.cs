using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudLogin.Models;

namespace StudLogin.Controllers
{
    public class logController : Controller
    {
        // GET: log
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: log/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: log/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: log/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: log/Edit/5
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

        // GET: log/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: log/Delete/5
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
