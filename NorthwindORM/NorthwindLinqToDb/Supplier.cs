﻿using System.Collections.Generic;
using LinqToDB.Mapping;

namespace NorthwindLinqToDb
{
    [Table("[dbo].[Suppliers]")]
    public class Supplier
    {
        [Column("SupplierID")]
        [PrimaryKey]
        [Identity]
        public int SupplierId { get; set; }

        [Column("CompanyName")]
        [NotNull]
        public string CompanyName { get; set; }

        [Column("ContactName")]
        public string ContactName { get; set; }

        [Association(ThisKey = nameof(SupplierId), OtherKey = nameof(Product.SupplierId), CanBeNull = true)]
        public IEnumerable<Product> Products { get; set; }
    }
}
