using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;


namespace WebApplication1.Controllers
{
    public class loginController : ApiController
    {
        [Route("api/login/Get")]
        public HttpResponseMessage Get()
        {
            string query = @"
                    select e_id,firstname,lastname,phone_no,position,age,salary,filename
                    from employee
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }
        // GET: login
        public string Post(Employee emp)
        {
            string query = @" select * from employee where firstname='" + emp.firstname + @"' ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@firstname", emp.firstname);


                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                    if (table.Rows.Count > 1)
                    {
                        return "Logged In";
                    }
                    else
                        return "Login Failed";
                }

            }

        }
    }
}