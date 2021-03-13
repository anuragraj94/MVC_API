using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace web_Api.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext():base ("DefaultConnection")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}