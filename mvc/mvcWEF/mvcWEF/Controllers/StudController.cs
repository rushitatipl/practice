using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using mvcWEF.Models;

namespace mvcWEF.Controllers
{
    public class StudController : Controller
    {
        string connectionString = @"data source=101.53.146.129;initial catalog=tipltrainee;user id=trainee;password=tipl#789;MultipleActiveResultSets=True;";
        // GET: Stud
        [HttpGet]
        
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from Student_Trainee",sqlcon);
                sda.Fill(dt);
            }
            return View(dt);
        }

        // GET: Stud/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stud/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new studentModel());
        }

        // POST: Stud/Create
        [HttpPost]
        public ActionResult Create(studentModel studmodel)
        {

            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "insert into Student_Trainee ([name],[email],[gender],[dob]) VALUES(@name,@email,@gender,@dob)";


                SqlCommand sqcmd = new SqlCommand(query,sqlcon);
                sqcmd.Parameters.AddWithValue("@name",studmodel.name);
                sqcmd.Parameters.AddWithValue("@email", studmodel.email);
                sqcmd.Parameters.AddWithValue("@gender", studmodel.gender);
                sqcmd.Parameters.AddWithValue("@dob", studmodel.dob);
                sqcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
            
        }

        // GET: Stud/Edit/5
        public ActionResult Edit(int id)
        {
            studentModel studmodel = new studentModel();
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "select * from Student_trainee where id=@id";
                SqlDataAdapter sqcmd = new SqlDataAdapter(query, sqlcon);
                sqcmd.SelectCommand.Parameters.AddWithValue("@id", id);
                sqcmd.Fill(dt);
            }
            if (dt.Rows.Count==1)
            {
                studmodel.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                studmodel.name = (dt.Rows[0]["name"].ToString());
                studmodel.email = (dt.Rows[0]["email"].ToString());
                studmodel.gender = (dt.Rows[0]["gender"].ToString());
                studmodel.dob = Convert.ToDateTime(dt.Rows[0]["dob"].ToString());
                return View(studmodel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Stud/Edit/5
        [HttpPost]
        public ActionResult Edit(studentModel studmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "update Student_Trainee SET name=@name,email=@email,gender=@gender,dob=@dob where id=@id";
                SqlCommand sqcmd = new SqlCommand(query, sqlcon);
                sqcmd.Parameters.AddWithValue("@id", studmodel.id);
                sqcmd.Parameters.AddWithValue("@name", studmodel.name);
                sqcmd.Parameters.AddWithValue("@email", studmodel.email);
                sqcmd.Parameters.AddWithValue("@gender", studmodel.gender);
                sqcmd.Parameters.AddWithValue("@dob", studmodel.dob);
                sqcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Stud/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "delete from Student_Trainee where id=@id";
                SqlCommand sqcmd = new SqlCommand(query, sqlcon);
                sqcmd.Parameters.AddWithValue("@id",id);
               
                sqcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Stud/Delete/5
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
