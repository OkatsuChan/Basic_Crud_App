using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApp_SqlProject.Models
{
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } //users is the table in sql

        public UserDBContext(): base("name=YourConnectionString") { } // to get connection
    }
}