using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineSite.Models;

namespace onlineSite.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            using(DbModel db=new DbModel())
            {
                return View(db.products.ToList());
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}