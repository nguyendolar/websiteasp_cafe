using System.Data.Entity;

namespace websitecafe.Models
{
    public class DBWebcafeContext : DbContext
    {
        public DBWebcafeContext() : base("DBConnectionString") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}
