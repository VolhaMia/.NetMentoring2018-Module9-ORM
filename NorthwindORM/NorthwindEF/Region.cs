using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NorthwindEF
{
    [Table("Regions")]
    public class Region
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        public int RegionID { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }

        public ICollection<Territory> Territories { get; set; }
    }
}
