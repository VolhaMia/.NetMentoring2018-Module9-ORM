using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NorthwindEF
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext()
            : base("name=Northwind")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}
