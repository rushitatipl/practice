using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;
using login_signup.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.Web.Helpers;



/* 
    @Controller UserController
    @Parama otp as integer
    @Author1: EMP1 - TIPL
    @Author2: EMP2 - TIPL
    @Date:    03-Nov-2021
    @Description : This is UserController for Login and Signup With OTP functionallity
    @AdditionalCheck : Null
    @Return :  Redirect on Specific View As per Controller logic

    */
namespace login_signup.Controllers
{
    
    public class UserController : Controller
    {
        string Connection = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        [HttpGet]
        // GET: User
        
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(Connection))
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from logandreg", sqlcon);
                sda.Fill(dt);
            }
            return View(dt);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        /* 
      @function IsValidUser
      @Parama otp as integer
      @Author1: EMP1 - TIPL
      @Author2: EMP2 - TIPL
      @Date:    03-Nov-2021
      @Description : verify OTP
      @AdditionalCheck : Null
      @Return : Null

      */

        private bool IsValidUser(int otp)
        {
            var mail = Session["email"];
            bool IsValid = false;
            SqlConnection sqlcon = new SqlConnection(Connection);
            string query = "select Otp from logandreg where Email='" + mail + "'";
           
            DataTable dt = new DataTable();

            if (ConnectionState.Closed == sqlcon.State)
            {
                sqlcon.Open();
            }
            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            //  Error = null;
            //  return dt;


            if (dt.Rows.Count > 0)
            {
                int dbotp = Convert.ToInt32(dt.Rows[0]["Otp"]);
                if (otp == dbotp)
              {
                    IsValid = true;
                }

            }


            return IsValid;
        }

        /* 
      @function IsLoginUser
      @Parama emailId as String,Password as string
      @Author1: EMP1 - TIPL
      @Author2: EMP2 - TIPL
      @Date:    03-Nov-2021
      @Description : check username and password is valid or not at login time
      @AdditionalCheck : Null
      @Return : Null

      */
        private bool IsLoginUser(string Email, string Password)
        {
           // bool active = true;
            SqlConnection sqlcon = new SqlConnection(Connection);
            bool IsValid = false;
            string query = "select * from logandreg where Email=@email and Password=@Password and IsEmailVerified=1";
            
            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sqlcon.Open();
                int i = cmd.ExecuteNonQuery();
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        [HttpGet]
        public ActionResult loginUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult loginUser(registerModel model)
        {
            
          
                if (IsLoginUser(model.Email, model.Password))
                {
                    
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.msg = "Your Email and password is incorrect";
                    return View();

                }
            
            
         
        }

        public ActionResult welcome()
        {
            return View();
        }
       
        public ActionResult otppass(string text)
        {
            if (text == "process")
            {
                resendOtp();
                return View();
            }


            return View(new registerModel());
        }
        

        [HttpPost]
        public ActionResult otppass(registerModel model, string text)
        {
            if (model.Otp == 0 )
            {
                resendOtp();
                return View();
            }
           
            if (IsValidUser(model.Otp))
            {
                
                string _Email = Session["email"].ToString();
                bool status = true;
                string strOtpUpdateQuery = "update logandreg set IsEmailVerified='" + status + "' where Email='" + _Email + "'";
                Update(strOtpUpdateQuery);
                Session["isActive"] = model.IsEmailVerified;
                return RedirectToAction("welcome");

              
            }

            ModelState.Clear();
            return View();
           
        }

        /* 
       @function sendvarificationlink
       @Parama emailId as String,otp as Integer
       @Author1: EMP1 - TIPL
       @Author2: EMP2 - TIPL
       @Date:    03-Nov-2021
       @Description : Send OTP to user for Login
       @AdditionalCheck : Null
       @Return : Null

       */

        [NonAction]
        [ValidateAntiForgeryToken]
        public void sendvarificationlink(string emailId, int otp)
        {

            var verifyUrl = "/User/" + "VerifyAccount" + "/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("tathyainfotechtest@gmail.com", "Online Shop");
            var toEmail = new MailAddress(emailId);
            var fromEmailpassword = "Tipl@12345!";

            string subject = "";
            string body = "";

            subject = "Your Account is successfully Created.";
            body = "<br/><br/>We are excited to tell you that your online Shop Acccount is " +
                 "Successfully created . Please click on the below link Verify your account" +
                 "<br/><br/><a href='" + otp + "'>" + otp + "</a>";

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

        public ActionResult CreateRegister()
        {
            return View(new registerModel());
        }

        //POST: User/Create
        [HttpPost]
        public ActionResult CreateRegister([Bind(Exclude = "Otp")] registerModel regmodel)
        {
            string Massege = "";
            bool Status = false;
            if (IsEmailExist(regmodel.Email))
            {

                using (SqlConnection sqlcon = new SqlConnection(Connection))
                {
                    Random rdm = new Random();
                    int otp = Convert.ToInt32(rdm.Next(100000, 999900).ToString());


                    sqlcon.Open();
                    string query = "insert into logandreg ([Firstname],[Lastname],[Mobile],[Email],[Gender],[Password],[Cpassword],[Profile],[Otp]) VALUES(@Firstname,@Lastname,@Mobile,@Email,@Gender,@Password,@cPassword,@Profile,@Otp)";


                    SqlCommand sqcmd = new SqlCommand(query, sqlcon);

                    sqcmd.Parameters.AddWithValue("@Firstname", regmodel.Firstname);
                    sqcmd.Parameters.AddWithValue("@Lastname", regmodel.Lastname);
                    sqcmd.Parameters.AddWithValue("@Mobile", regmodel.Mobile);
                    sqcmd.Parameters.AddWithValue("@Email", regmodel.Email);
                    sqcmd.Parameters.AddWithValue("@Gender", regmodel.Gender);
                    sqcmd.Parameters.AddWithValue("@Password", regmodel.Password);
                    sqcmd.Parameters.AddWithValue("@Cpassword", regmodel.Cpassword);
                    sqcmd.Parameters.AddWithValue("@Profile", regmodel.Profile);
                    sqcmd.Parameters.AddWithValue("@Otp", otp);



                    // FormsAuthentication.SetAuthCookie(regmodel.Email, true);
                    Session["email"] = regmodel.Email.ToString();
                    sqcmd.ExecuteNonQuery();



                    sendvarificationlink(regmodel.Email, otp);
                    Massege = "registation Successfully Done . Account activation link " +
                        "has been sent to your email id:" + regmodel.Email;
                    Status = true;

                    // updateOtp(regmodel.Email);


                }
            }
            else
            {
               
                    ViewBag.msg = "Email Already Exists";
                    return View(regmodel);
                
            }
            return RedirectToAction("otppass");
        }

        /* 
       @function IsEmailExist
       @Parama emailId as String
       @Author1: EMP1 - TIPL
       @Author2: EMP2 - TIPL
       @Date:    03-Nov-2021
       @Description : check  input email is already exist or not
       @AdditionalCheck : Null
       @Return : Null

       */

        [NonAction]
        public bool IsEmailExist(string Email)
        {
            SqlConnection sqlcon = new SqlConnection(Connection);
            SqlCommand cmd = new SqlCommand("select Email from  logandreg where email='" + Email + "'", sqlcon);

            sqlcon.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /* 
       @function userActive
       @Parama :Model as RegistrModel
       @Author1: EMP1 - TIPL
       @Author2: EMP2 - TIPL
       @Date:    03-Nov-2021
       @Description : if OTP verify,isActive User State is true
       @AdditionalCheck : Null
       @Return : Null

       */

        [NonAction]
        public void userActive(registerModel model)
        {
            string _Email = Session["email"].ToString();
            
            bool status = true;
            string strOtpUpdateQuery = "update logandreg set IsEmailVerified='" + status + "' where Email='" + _Email + "'";

            Update(strOtpUpdateQuery);
           

        }

        /* 
      @function resendOtp
      @Author1: EMP1 - TIPL
      @Author2: EMP2 - TIPL
      @Date:    03-Nov-2021
      @Description : Resend OTP To User 
      @AdditionalCheck : Null
      @Return : Null

      */

        public void resendOtp()
        {
            string _Email = Session["email"].ToString();
            int _Otp = GenerateOTP();
            // OTP Updattion Start here

            string strOtpUpdateQuery = "update logandreg set Otp=" + _Otp + " where Email='" + _Email + "'";

            int _queryResponse = Update(strOtpUpdateQuery);
            if (_queryResponse == 1)
            {
                sendvarificationlink(_Email, _Otp);
            }

            // OTP Updation End

        }

        /* 
       @function Update
       @Parameter :strQuery as string
       @Author1: EMP1 - TIPL
       @Author2: EMP2 - TIPL
       @Date:    03-Nov-2021
       @Description : Reusable Update function ,Use for all update query
       @AdditionalCheck : Null
       @Return : Null

       */

        public int Update(string strQuery)
        {
            SqlConnection sqlcon = new SqlConnection(Connection);
            try
            {

                if (ConnectionState.Closed == sqlcon.State)
                {
                    sqlcon.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, sqlcon))
                {
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        /* 
       @function GenerateOTP
       @Author1: EMP1 - TIPL
       @Author2: EMP2 - TIPL
       @Date:    03-Nov-2021
       @Description : generate random OTP
       @AdditionalCheck : Null
       @Return : Random OTP

       */
        public int GenerateOTP()
        {
            Random rdm = new Random();
            string otp = rdm.Next(100000, 999900).ToString();
            return Convert.ToInt32(otp);
        }


        // POST: User/Edit/5
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
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("loginUser", "User");
        }
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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

