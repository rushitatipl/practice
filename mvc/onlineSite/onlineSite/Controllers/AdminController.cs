using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using onlineSite.Models;
using onlineSite.Repository;

namespace onlineSite.Controllers
{
    
    public class AdminController : Controller
    {
      
        public GenericWork _genericWork = new GenericWork();
         //GET: Admin
        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult categoty()
        {
            List<category> allcategories = _genericWork.GetRepositoryInstance<category>().GetAllRecordIQueryable().ToList();
            return View(allcategories);
        }
    
        public ActionResult AddCategory(customer_reg reg)
        {
            return UpdateCategory(0);
        }
      
        public ActionResult UpdateCategory(int cateId)
        {
            categotyDetail cd;
            if (cateId != null)

            {
                cd = JsonConvert.DeserializeObject<categotyDetail>(JsonConvert.SerializeObject(_genericWork.GetRepositoryInstance<category>().getFirstOrDeafault(cateId)));
            }
            else
            {
                cd = new categotyDetail();
            }
            return View("UpdateCategory", cd);
        }

        public ActionResult product()
        {
            return View(_genericWork.GetRepositoryInstance<product>().GetProduct());
        }
        [HttpGet]
        public ActionResult productEdit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult productEdit(int productId)
        {
            return View(_genericWork.GetRepositoryInstance<product>().getFirstOrDeafault(productId));
        }

    }
}