using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentReg.Models;

namespace StudentReg.Controllers
{
    public class RegController : Controller
    {
        // GET: Reg
        public ActionResult Index()
        {
            using(DBModel dm=new DBModel()) 
            { 
            return View(dm.Regs.ToList());
            }
        }

        // GET: Reg/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reg/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reg/Create
        [HttpPost]
        public ActionResult Create(Reg stude)
        {
            try
            {
                using (DBModel db=new DBModel())
                {
                    db.Regs.Add(stude);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reg/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reg/Edit/5
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

        // GET: Reg/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reg/Delete/5
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
