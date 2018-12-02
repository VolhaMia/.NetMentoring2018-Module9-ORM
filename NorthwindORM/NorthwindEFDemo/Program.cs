using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindEF;

namespace NorthwindEFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new NorthwindContext();

            int selectedCategoryId = 1;
            var query = db.Orders.Include(o => o.OrderDetails.Select(od => od.Product)).Include(o => o.Customer)
                .Where(o => o.OrderDetails.Any(od => od.Product.CategoryID == selectedCategoryId))
                .Select(o => new
                {
                    o.Customer.ContactName,
                    OrderDetails = o.OrderDetails.Select(od => new
                    {
                        od.Product.ProductName,
                        od.OrderID,
                        od.ProductID
                    })
                });
            var result = query.ToList();

            foreach (var row in result)
            {
                Console.WriteLine($"Customer: {row.ContactName} Products: {string.Join(", ", row.OrderDetails.Select(od => od.ProductName))}");
            }

            db.Dispose();
        }
    }
}
