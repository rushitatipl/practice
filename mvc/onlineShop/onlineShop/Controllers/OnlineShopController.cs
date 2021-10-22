using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlineShop.Controllers
{
    public class OnlineShopController : Controller
    {
        // GET: OnlineShop
        public ActionResult Index()
        {
            return View();
        }

        // GET: OnlineShop/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OnlineShop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OnlineShop/Create
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

        // GET: OnlineShop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OnlineShop/Edit/5
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

        // GET: OnlineShop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OnlineShop/Delete/5
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
