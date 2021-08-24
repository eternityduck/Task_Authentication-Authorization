using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TaskAuthenticationAuthorization.Models
{
    public class ShoppingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "buyer";

            string adminEmail = "admin@gmail.com";
            string adminPasswod = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role buyerRole = new Role { Id = 2, Name = userRoleName };
            User admin = new User { Id = 1, Email = adminEmail, Password = adminPasswod, RoleId = adminRole.Id, BuyerType = BuyerType.none};
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, buyerRole });
            modelBuilder.Entity<User>().HasData(new User[] { admin });
            base.OnModelCreating(modelBuilder);
        }
    }
}
