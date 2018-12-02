using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NorthwindEF
{
    [Table("Order Details")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductID { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
