using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal static class MyDbInitializer
    {
        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category() { Id = (int)CategoryTypes.Electronics, Name = "Electronics" },
                new Category() { Id = (int)CategoryTypes.Sport, Name = "Sport" },
                new Category() { Id = (int)CategoryTypes.FashionAndArt, Name = "Fashion & Art" },
                new Category() { Id = (int)CategoryTypes.HomAndGarder, Name = "Home & Garder" }
            });
        }

        public static void SeedProducts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product()
                {
                    Id = 1,
                    Name = "iPhone 13 Pro",
                    Price = 900,
                    CategoryId = (int)CategoryTypes.Electronics
                },
                new Product()
                {
                    Id = 2,
                    Name = "Table tennis ball",
                    Price = 15,
                    CategoryId = (int)CategoryTypes.Sport
                }
            });
        }
    }
}
