using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using onlineSite.Models;


namespace onlineSite.Controllers
{
    public class onlineController : Controller
    {
        // GET: online
        [Authorize]
        public ActionResult Index(product pro)
        {
            TempData["Category"] = pro;
            TimeZoneInfo timeZoneInfo;
            //DateTime dateTime;
            //Set the time zone information
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Get date and time
            ViewBag.dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            // return View();
            // GetIPAddress2();
           
            string ip = Request.UserHostAddress;
            ViewBag.ip = ip;
            using (DbModel db = new DbModel())
            
            {
               
                return View(db.products.ToList());
            }
           
        }
        
        // GET: online/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.products.Where(a => a.p_id == id).FirstOrDefault());
            }
        }

        // GET: online/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        private string GenerateOtp()
        {
            int min = 100000;
            int max = 999900;
            int otp = 0;
            Random rdm = new Random();
            otp = rdm.Next(min, max);
            return otp.ToString();
            // return "123456";

        }

        public ActionResult Buy(int id)
        { DbModel db = new DbModel();
           // productModel productModel = new productModel();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item{Product1 = db.products.Find(id), Quantity = 1}) ;
               // cart.Add(db.productts.Where(a=>a.p_id==id).FirstOrDefault());
               Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product1 = db.products.Find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            // return RedirectToAction("Index");
            return View();
        }
        public ActionResult Remove(int id)
        
            
   
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            if (index != 1)
            {
                cart[index].Quantity--;
                
            }
            else
            {
                cart.RemoveAt(index);
                Session["cart"] = cart;
                //return RedirectToAction("Buy");
                return View("Buy");
            }
            return View("Buy");
        }   
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product1.p_id.Equals(id))
                    return i;
            return -1;
        }
       
        // POST: online/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Exclude = "IsEmailVerified,ActivationCode")] customer_reg reg)
        {
            bool Status = false;
            string Message = "";
            //model validation
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(reg.email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exists");
                    return View(reg);
                }

                //Generate Activation Code
                #region Generate Activation Code
                reg.ActivationCode = Guid.NewGuid();
                #endregion

                //Password Hashing
                #region Password Hashing
                reg.password = Crypto.Hash(reg.password);
                reg.cpassword = Crypto.Hash(reg.cpassword);
                #endregion

                #region Save To Database
                using (DbModel db = new DbModel())
                {
                    db.customer_reg.Add(reg);
                    db.SaveChanges();
                    //Send email to user
                    sendvarificationlink(reg.email, reg.ActivationCode.ToString());
                    Message = "registation Successfully Done . Account activation link " +
                        "has been sent to your email id:" + reg.email;
                    Status = true;

                }
                #endregion
            }
            else
            {
                Message = "InValid Email";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View();
            //try
            //{
            //    using (DbModel db = new DbModel())
            //    {
            //        db.customer_reg.Add(reg);
            //        db.SaveChanges();

            //    }
            //    return RedirectToAction("CreateLogin");
            //}
            //catch
            //{
            //    return View();
            //}
        }
       

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (DbModel dc = new DbModel())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = dc.customer_reg.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }




        [NonAction]
        public bool IsEmailExist(string EmailId)
        {
            using (DbModel db = new DbModel())
            {
                var v = db.customer_reg.Where(a => a.email == EmailId).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void sendvarificationlink(string emailId, string activationcode,string emailFor= "VerifyAccount")
        {
            //var scheme = Request.Url.Scheme;
            //var host = Request.Url.Host;

            var verifyUrl = "/online/"+ emailFor + "/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("tathyainfotechtest@gmail.com", "Online Shop");
            var toEmail = new MailAddress(emailId);
            var fromEmailpassword = "Tipl@12345!";

            string subject = "";
            string body = "";
            if (emailFor== "VerifyAccount")
            {
                subject = "Your Account is successfully Created.";
               body = "<br/><br/>We are excited to tell you that your online Shop Acccount is " +
                    "Successfully created . Please click on the below link Verify your account" +
                    "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>We got request for reset password.Please click on the below link to reset your password" +
                    "<br/><br/><a href='" + link + "'> Reset Password </a>";
            }

           

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailpassword)

            };
            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
                smtp.Send(Message);



        }

        public ActionResult productView()
        {
            using (DbModel db = new DbModel())
            {
                // return View(db.products.ToList());
                ViewBag.lstproduct = db.products.ToList();
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult CreateLogin()
        {
            return View();
        }

        // POST: online/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLogin(Login1 reg, string ReturnUrl)
        {
            customer_reg creg = new customer_reg();
            string Message = "";
            using (DbModel db = new DbModel())
            {
                var v = db.customer_reg.Where(a => a.email == reg.email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(reg.password), v.password) == 0)
                    {
                        int timeout = reg.remember ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(reg.email, reg.remember, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        Session["user"] = reg.email;

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                           // return Redirect("Index");
                        }
                        else
                        {
                           // return Redirect("Index"); 
                            return RedirectToAction("Index", "online");
                        }
                    }
                    else
                    {
                        Message = "Invalid";
                    }
                }
                else
                {
                    Message = "Invalid";
                }
            }
            ViewBag.message = Message;
            return View();
            //try
            //{
            //    using (DbModel db = new DbModel())
            //    {
            //        if (ModelState.IsValid) { 
            //        db.customer_reg.Add(reg);
            //        db.SaveChanges();
            //    }

            //    }

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("CreateLogin", "online");
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        //Forgot passeord
        [HttpPost]
        public ActionResult ForgotPassword(string emailId)
        {
            string Message = "";
            bool Status = false;
            using (DbModel db = new DbModel())
            {
                var account = db.customer_reg.Where(a => a.email == emailId).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    sendvarificationlink(account.email, resetCode,"ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
                else
                {
                    Message = "Account not found ";
                }
                return View();
            }
        }

        
        [NonAction]
        public  string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        //Reset password

        public ActionResult ResetPassword(string id)
        {
            using (DbModel db = new DbModel())
            {
                var user = db.customer_reg.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        //Reset Password Model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";

            ViewBag.Message = message;
            if (ModelState.IsValid)
            {
                using(DbModel db=new DbModel())
                {
                    var user= db.customer_reg.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New Password updated successfully";

                    }
                }
            }
            else
            {
                message = "Something inValid";
            }
            return View(model);
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
       
        public ActionResult addtocart(int ?productid, product pi)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.products.Where(a => a.p_id == productid).FirstOrDefault());
            }
            //DbModel db = new DbModel();
            //return View(db.products.Where(a => a.p_id == productid).FirstOrDefault());
            //if (TempData["Category"] != null)
            //{
            //    var Cat = (product)TempData["Category"];



            //    var Productss = from p in db.products
            //                    where p.p_id == productid
            //                    select p;
            //    //List<product> cart = new List<product>();
            //    //cart.Add(db.products.Where(a => a.p_id == productid).FirstOrDefault());
            //    //TempData["cart"] = cart;

            //    return View(Productss.ToList());
            //}
            //else
            //{
            //    return View(db.products.ToList());
            //}
        }
       

        [HttpPost]
        public ActionResult addtocart(int productid,string quantity, product pi)
        {
            var Data = TempData["Category"] as category;

          
            return RedirectToAction("Index");
        }
        // GET: online/Delete/5
        public ActionResult Delete(int id)
        {
            using(DbModel db=new DbModel())
            {
                return View(db.customer_reg.Where(x => x.customer_id == id).FirstOrDefault());
            }
        }

        // GET: online/contact/5
        public ActionResult contact(int id)
        {
            return View();
        }

        // POST: online/conatact/5
        [HttpPost]
        public ActionResult contact(int id,contactU contact)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.contactUs.Add(contact);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: online/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DbModel db = new DbModel())
                {
                    customer_reg cust= db.customer_reg.Where(x => x.customer_id == id).FirstOrDefault();
                    db.customer_reg.Remove(cust);
                    db.SaveChanges();
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
