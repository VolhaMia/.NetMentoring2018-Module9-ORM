using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using NorthwindLinqToDb;

namespace NortwindLinqToDbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var _connection = new NorthwindConnection("Northwind");

            Console.WriteLine("Task2:");

            Console.WriteLine("\nProducts List with Category and Supplier:");

            foreach (var product in _connection.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToList())
            {
                Console.WriteLine($"Product name: {product.ProductName}; Category: {product.Category?.CategoryName}; Supplier: {product.Supplier?.ContactName}");
            }

            Console.WriteLine("\nEmployees List with Region:");

            var query = from e in _connection.Employees
                join et in _connection.EmployeeTerritories on e.EmployeeId equals et.EmployeeId into el
                from w in el.DefaultIfEmpty()
                join t in _connection.Territories on w.TerritoryId equals t.TerritoryId into zl
                from z in zl.DefaultIfEmpty()
                join r in _connection.Regions on z.RegionId equals r.RegionId into kl
                from k in kl.DefaultIfEmpty()
                select new { e.FirstName, e.LastName, Region = k };
            query = query.Distinct();

            foreach (var record in query.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName}; Region: {record.Region?.RegionDescription}");
            }

            Console.WriteLine("\nEmployees Statistics by Regions:");

            var query2 = from r in _connection.Regions
                join t in _connection.Territories on r.RegionId equals t.RegionId into kl
                from k in kl.DefaultIfEmpty()
                join et in _connection.EmployeeTerritories on k.TerritoryId equals et.TerritoryId into zl
                from z in zl.DefaultIfEmpty()
                join e in _connection.Employees on z.EmployeeId equals e.EmployeeId into dl
                from d in dl.DefaultIfEmpty()
                select new { Region = r, d.EmployeeId };
            var result = from row in query2.Distinct()
                group row by row.Region into ger
                select new { RegionDescription = ger.Key.RegionDescription, EmployeesCount = ger.Count(e => e.EmployeeId != 0) };

            foreach (var record in result.ToList())
            {
                Console.WriteLine($"Region: {record.RegionDescription}; Employees count: {record.EmployeesCount}");
            }

            Console.WriteLine("\nEmployees Shippers according to Orders:");

            var query3 = (from e in _connection.Employees
                join o in _connection.Orders on e.EmployeeId equals o.EmployeeId into el
                from w in el.DefaultIfEmpty()
                join s in _connection.Shippers on w.ShipperId equals s.ShipperId into zl
                from z in zl.DefaultIfEmpty()
                select new { e.EmployeeId, e.FirstName, e.LastName, z.CompanyName }).Distinct().OrderBy(t => t.EmployeeId);

            foreach (var record in query3.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName} Shipper: {record.CompanyName}");
            }

            Console.WriteLine("\nTask3:");

            //Add new Employee with Territories

            Employee newEmployee = new Employee { FirstName = "Meshkova", LastName = "Olga" };
            try
            {
                _connection.BeginTransaction();
                newEmployee.EmployeeId = Convert.ToInt32(_connection.InsertWithIdentity(newEmployee));
                _connection.Territories.Where(t => t.TerritoryDescription.Length <= 5)
                    .Insert(_connection.EmployeeTerritories, t => new EmployeeTerritory { EmployeeId = newEmployee.EmployeeId, TerritoryId = t.TerritoryId });
                _connection.CommitTransaction();
            }
            catch
            {
                _connection.RollbackTransaction();
            }

            Console.WriteLine("\nMove Products to another Category:");

            int updatedCount = _connection.Products.Update(p => p.CategoryId == 2, pr => new Product
            {
                CategoryId = 1
            });

            Console.WriteLine($"{updatedCount} products updated with new category");

            //Add Products list with Suppliers and Categories;

            var products = new List<Product>
            {
                new Product
                {
                    ProductName = "MyNewProduct",
                    Category = new Category {CategoryName = "MyNewCategory"},
                    Supplier = new Supplier {CompanyName = "MyNewSupplier"}
                },
                new Product
                {
                    ProductName = "MyNewProduct2",
                    Category = new Category {CategoryName = "MyNewCategory2"},
                    Supplier = new Supplier {CompanyName = "MyNewSupplier2"}
                }
            };

            try
            {
                _connection.BeginTransaction();

                foreach (var product in products)
                {
                    var category = _connection.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                    product.CategoryId = category?.CategoryId ?? Convert.ToInt32(_connection.InsertWithIdentity(
                                             new Category
                                             {
                                                 CategoryName = product.Category.CategoryName
                                             }));
                    var supplier = _connection.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
                    product.SupplierId = supplier?.SupplierId ?? Convert.ToInt32(_connection.InsertWithIdentity(
                                             new Supplier
                                             {
                                                 CompanyName = product.Supplier.CompanyName
                                             }));
                }

                _connection.BulkCopy(products);
                _connection.CommitTransaction();
            }
            catch
            {
                _connection.RollbackTransaction();
            }

            Console.WriteLine("\nReplacing Product with analog null ShippedDate:");

            var updatedRows = _connection.OrderDetails.LoadWith(od => od.Order).LoadWith(od => od.Product)
                .Where(od => od.Order.ShippedDate == null).Update(
                    od => new OrderDetail
                    {
                        ProductId = _connection.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId) != null
                            ? _connection.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId).ProductId
                            : _connection.Products.First(p => p.CategoryId == od.Product.CategoryId).ProductId
                    });
            Console.WriteLine($"{updatedRows} rows updated");

            _connection.Dispose();
        }
    }
}
