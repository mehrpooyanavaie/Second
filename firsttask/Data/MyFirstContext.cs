using firsttask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace firsttask.Data
{
    public class MyFirstContext : DbContext
    {
        public MyFirstContext(DbContextOptions<MyFirstContext> options) : base(options)
        {
        }
        public DbSet<firsttask.Models.Category> Categories { get; set; }
        public DbSet<firsttask.Models.Product> Products { get; set; }
        public DbSet<firsttask.Models.Resid> Resids { get; set; }
        public DbSet<firsttask.Models.Havale> Havales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var lebas = new Category() { Id = 1, CategoryName = "لباس" };
            var shalvar = new Category() { Id = 2, CategoryName = "شلوار" };
            modelBuilder.Entity<Models.Category>().HasData(lebas, shalvar);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                Id = 1,
                ProductName = "پیراهن مردانه",
                CategoryId = lebas.Id,
                ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0)
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                Id = 2,
                ProductName = "لی",
                CategoryId = shalvar.Id,
                ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0)
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                Id = 3,
                ProductName = "پیراهن زنانه",
                CategoryId = lebas.Id,
                ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0)
            });
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                Id = 4,
                ProductName = "کتان",
                CategoryId = shalvar.Id,
                ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0)
            });
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                Id = 5,
                ProductName = "هودی",
                CategoryId = lebas.Id,
                ExpireTime = new DateTime(2024, 3, 23, 10, 30, 0)
            });
        }
    }
}

