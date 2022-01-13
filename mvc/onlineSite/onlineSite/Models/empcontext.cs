using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace onlineSite.Models
{
    public class empcontext:DbContext
    {
        public empcontext() : base("DbModel")
        {
            
        }
        public DbSet<emp> emps { get; set; }
    }
}