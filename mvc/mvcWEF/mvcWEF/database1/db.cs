using System;
using System.Data;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace mvcWEF.database1
{
    public class db
    {
        // string connectionString = @"data source=101.53.146.129;initial catalog=tipltrainee;user id=trainee;password=tipl#789;MultipleActiveResultSets=True;";
        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataSet show_data()
        {
            SqlCommand cmd = new SqlCommand("select * from Student_Trainee", sqlcon);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet da = new DataSet();
            sda.Fill(da);
            return da;
        }
    }
}