using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web.Http;

using System.Net.Http.Headers;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class DepartmentController : ApiController
    {

        //public HttpResponseMessage Get() 
        //{
        //    string query = @"
        //            select departmentid,departmentname from
        //            Department
        //            ";
        //    DataTable table = new DataTable();
        //    using(var con= new SqlConnection(ConfigurationManager.
        //        ConnectionStrings["EmployeeAppDB"].ConnectionString))
        //        using(var cmd= new SqlCommand(query,con))
        //    using (var da = new SqlDataAdapter(cmd))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        da.Fill(table);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, table);


        //}

        //public string Post(Department dep)
        //{
        //    try
        //    {
        //        string query = @"
        //            insert into dbo.Department values
        //            ('"+dep.DepartmentName+ @"')
        //            ";

        //        DataTable table = new DataTable();
        //        using (var con = new SqlConnection(ConfigurationManager.
        //            ConnectionStrings["EmployeeAppDB"].ConnectionString))
        //        using (var cmd = new SqlCommand(query, con))
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            da.Fill(table);
        //        }

        //        return "Added Successfully!!";
        //    }
        //    catch (Exception)
        //    {

        //        return "Failed to Add!!";
        //    }
        //}


        //public string Put(Department dep)
        //{
        //    try
        //    {
        //        string query = @"
        //            update dbo.Department set DepartmentName=
        //            '" + dep.DepartmentName + @"'
        //            where DepartmentId="+dep.DepartmentId+@"
        //            ";

        //        DataTable table = new DataTable();
        //        using (var con = new SqlConnection(ConfigurationManager.
        //            ConnectionStrings["EmployeeAppDB"].ConnectionString))
        //        using (var cmd = new SqlCommand(query, con))
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            da.Fill(table);
        //        }

        //        return "Updated Successfully!!";
        //    }
        //    catch (Exception)
        //    {

        //        return "Failed to Update!!";
        //    }
        //}


        //public string Delete(int id)
        //{
        //    try
        //    {
        //        string query = @"
        //            delete from dbo.Department 
        //            where DepartmentId=" + id + @"
        //            ";

        //        DataTable table = new DataTable();
        //        using (var con = new SqlConnection(ConfigurationManager.
        //            ConnectionStrings["EmployeeAppDB"].ConnectionString))
        //        using (var cmd = new SqlCommand(query, con))
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            da.Fill(table);
        //        }

        //        return "Deleted Successfully!!";
        //    }
        //    catch (Exception)
        //    {

        //        return "Failed to Delete!!";
        //    }
        //}


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

        [HttpGet]
        [Route("GetImage")]
        //download Image file api  
        public HttpResponseMessage GetImage(string image)
        {
            //Create HTTP Response.  
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //Set the File Path.  
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Image/") + image + ".PNG";
            //Check whether File exists.  
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.  
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", image);
                throw new HttpResponseException(response);
            }
            //Read the File into a Byte Array.  
            byte[] bytes = File.ReadAllBytes(filePath);
            //Set the Response Content.  
            response.Content = new ByteArrayContent(bytes);
            //Set the Response Content Length.  
            response.Content.Headers.ContentLength = bytes.LongLength;
            //Set the Content Disposition Header Value and FileName.  
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = image + ".PNG";
            //Set the File Content Type.  
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(image + ".PNG"));
            return response;
        }

        // GET: login
        public string Post(Employee emp)
        {
            string mail = "admin@gmail.com";
            int ps = 1234;
            string query = @" select lastname from employee where email='" + emp.email + @"' and password='" + emp.password + @"' ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                //cmd.Parameters.AddWithValue("@firstname", emp.firstname);


                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                    if (table.Rows.Count >= 1)
                    {
                       
                       return "Logged In";
                    }
                    else if(emp.email == mail && emp.password == ps)
                    {
                        return "Admin Logged In";
                    }
                    
                    else
                        return "Login Failed";
                }
                
            }

        }


    }
}
