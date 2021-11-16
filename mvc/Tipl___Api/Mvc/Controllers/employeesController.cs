using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class employeesController : Controller
    {
        // GET: employees
        public ActionResult Index()
        {
            try
            {
                IEnumerable<mvcEmpModel> empList;
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("employees").Result;
                empList = response.Content.ReadAsAsync<IEnumerable<mvcEmpModel>>().Result;
                return View(empList);
            }
            catch(Exception Ex)
            {
                return View();
            }
           
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcEmpModel());
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmpModel>().Result);

            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcEmpModel emp)
        {
            if (emp.e_id == 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("employees", emp).Result;
                //TempData["successmessage"] = response;

                string str = "";
               

            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("employees/" + emp.e_id, emp).Result;
                TempData["successmessage"] = "Updated data successfully...";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("employees/" + id.ToString()).Result;
            TempData["successmessage"] = "Deleted data successfully...";

            return RedirectToAction("Index");
        }
    }
}