using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NorthwindEF
{
    public class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
            CreditCards = new HashSet<CreditCard>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Territory> Territories { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }
    }
}
