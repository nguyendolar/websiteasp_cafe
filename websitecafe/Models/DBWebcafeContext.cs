using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace websitecafe.Models
{
	public class DBWebcafeContext : DbContext
    {
        public DBWebcafeContext() : base("DBConnectionString")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}