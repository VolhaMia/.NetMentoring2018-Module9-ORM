using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace NorthwindLinqToDb
{
    [Table("[dbo].[Orders]")]
    public class Order
    {
        [Column("OrderID")]
        [Identity]
        [PrimaryKey]
        public int OrderId { get; set; }

        [Column("ShipVia")]
        public int? ShipperId { get; set; }

        [Column("ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        [Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(NorthwindLinqToDb.Employee.EmployeeId), CanBeNull = true)]
        public Employee Employee { get; set; }

        [Association(ThisKey = nameof(ShipperId), OtherKey = nameof(NorthwindLinqToDb.Shipper.ShipperId), CanBeNull = true)]
        public Shipper Shipper { get; set; }

    }
}
