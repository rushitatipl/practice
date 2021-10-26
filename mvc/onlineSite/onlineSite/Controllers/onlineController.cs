﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineSite.Models;
namespace onlineSite.Controllers
{
    public class onlineController : Controller
    {
        // GET: online
        public ActionResult Index()
        {
            TimeZoneInfo timeZoneInfo;
            //DateTime dateTime;
            //Set the time zone information
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time
            ViewBag.dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            using (DbModel db=new DbModel())
            {

                return View(db.customer_reg.ToList());
            }
           
        }

        // GET: online/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: online/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: online/Create
        [HttpPost]
        public ActionResult Create(customer_reg reg)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.customer_reg.Add(reg);
                    db.SaveChanges();
                   
                }
                return RedirectToAction("CreateLogin");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult productView()
        {
            using (DbModel db = new DbModel())
            {
                return View(db.products.ToList());
            }

        }

        public ActionResult CreateLogin()
        {
            return View();
        }

        // POST: online/Create
        [HttpPost]
        public ActionResult CreateLogin(customer_reg reg)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    if (ModelState.IsValid) { 
                    db.customer_reg.Add(reg);
                    db.SaveChanges();
                }

                }
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: online/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: online/Edit/5
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

        public ActionResult addtocart(int productid)
        {
            DbModel db = new DbModel();
            List<Item>  cart = new List<Item>();
            var prod = db.products.Find(productid);
            
            cart.Add(new Item()
            {
                pro = prod,
                quantity=1
               
            }) ;
            Session["cart"] = cart;
            return Redirect("Index");
        }
        // GET: online/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: online/Delete/5
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
