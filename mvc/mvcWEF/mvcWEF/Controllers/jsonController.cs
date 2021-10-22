using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using mvcWEF.Models;
namespace mvcWEF.Controllers

{
    public class jsonController : Controller
    {
        database1.db dbl = new database1.db();
        // GET: json
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult get_data()
        {
            DataSet ds = dbl.show_data();
            List<json> li = new List<json>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                li.Add(new json
                {
               
                name = dr["name"].ToString(),
                email = (dr["email"].ToString()),
                gender = (dr["gender"].ToString()),
                dob = Convert.ToDateTime( dr["dob"].ToString())
            });
            }
            return Json(li, JsonRequestBehavior.AllowGet);
        }

        // GET: json/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: json/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: json/Create
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

        // GET: json/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: json/Edit/5
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

        // GET: json/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: json/Delete/5
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
