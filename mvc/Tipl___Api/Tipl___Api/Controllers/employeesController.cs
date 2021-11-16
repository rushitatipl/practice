using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Tipl___Api.Models;

namespace Tipl___Api.Controllers
{
    public class employeesController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/employees
        public IQueryable<employee> Getemployees()
        {
            return db.employees;
        }

        // GET: api/employees/5
        [ResponseType(typeof(employee))]
        public HttpResponseMessage Getemployee(int id)
        {
            CommonResponse response = new CommonResponse();
            employee employee = db.employees.Find(id);

          //  employee.age=
            if (employee != null)
            {
                return Request.CreateResponse<employee>(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found");
            }
            
        }


        //public IHttpActionResult Add(EmployeeViewModel empmodel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid data.");

        //    using (DBModel db = new DBModel())
        //    {
        //        employee emp = new employee();
        //        emp.firstname = empmodel.firstname;
        //        emp.lastname = empmodel.lastname;
        //        emp.phone_no = empmodel.phone_no;
        //        emp.position = empmodel.position;
        //        emp.age = empmodel.age;
        //        emp.salary = empmodel.salary;
        //        db.employees.Add(emp);
        //        db.SaveChanges();
        //    }
        //    return Ok();

        //}

        // PUT: api/employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putemployee(int id, employee employee)
        {

            CommonResponse response = new CommonResponse();
            if (id != employee.e_id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeExists(id))
                {
                    response.Message = "Employee not Available..";

                    response.IsSuccess = true;
                    return Ok(response);
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/employees
        [ResponseType(typeof(employee))]
        [HttpPost]
        public IHttpActionResult Postemployee(employee employee)
        {
            if (employee == null)
            {
                return BadRequest("User details cannot be null.");
            }
            //CommonResponse response = new CommonResponse();
            db.employees.Add(employee);
            db.SaveChanges();
           // return Ok("Data Saved Successfully");
          

            return Ok(new { isSuccess = true, message = "Data Saved Successfully" });
           // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            //return response;
          // return CreatedAtRoute("DefaultApi", new { id = employee.e_id }, employee);

        }

        // DELETE: api/employees/5
        [ResponseType(typeof(employee))]
        public IHttpActionResult Deleteemployee(int id)
        {
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employeeExists(int id)
        {
            return db.employees.Count(e => e.e_id == id) > 0;
        }
    }
}