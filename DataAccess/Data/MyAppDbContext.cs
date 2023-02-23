using Core.Entities;
using Core.Entities;
using Infrastructure.Configurations;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class MyAppDbContext : IdentityDbContext<User>
    {
        public MyAppDbContext() : base() { }
        public MyAppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedCategories();
            modelBuilder.SeedProducts();

            // ---------------- apply Fuent API configurations
            modelBuilder.ApplyConfiguration(new ProductConfigs());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        // ----------------------- Collections
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}