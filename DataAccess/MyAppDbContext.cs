using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class MyAppDbContext : DbContext
    {
        public MyAppDbContext() : base() { }
        public MyAppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedCategories();
            modelBuilder.SeedProducts();
        }

        // ----------------------- Collections
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}