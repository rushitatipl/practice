using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspProjects.Models;
using System.Data.Entity;
namespace aspProjects.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            using(DbModel dbmodel =new DbModel())
            {
                return View(dbmodel.students.ToList());
            }
            
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel dbmodel = new DbModel())
            {
                return View(dbmodel.students.Where(x => x.studentID == id).FirstOrDefault());
            }
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(student std)
        {
            try
            {
                using (DbModel dbmodel = new DbModel())
                {
                    dbmodel.students.Add(std);
                    dbmodel.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel dbmodel = new DbModel())
            {
                //  return View(dbmodel.families.Where(x => x.memberid == id).FirstOrDefault());
                return View(dbmodel.students.Where(x => x.studentID == id).FirstOrDefault());
            }
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, student std)
        {
            try
            {
                // TODO: Add update logic here
                using (DbModel dbModels = new DbModel())
                {
                    dbModels.Entry(std).State = EntityState.Modified;
                    dbModels.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel dbmodel = new DbModel())
            {
                return View(dbmodel.students.Where(x => x.studentID == id).FirstOrDefault());
            }
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DbModel dbmodel = new DbModel())
                {
                    student std = dbmodel.students.Where(x => x.studentID == id).FirstOrDefault();
                    dbmodel.students.Remove(std);
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
