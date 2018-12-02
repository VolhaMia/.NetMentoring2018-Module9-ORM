﻿using LinqToDB.Mapping;

namespace NorthwindLinqToDb
{
    [Table("[dbo].[EmployeeTerritories]")]
    public class EmployeeTerritory
    {
        [Column("EmployeeID")]
        [NotNull]
        public int EmployeeId { get; set; }

        [Column("TerritoryID")]
        [NotNull]
        public int TerritoryId { get; set; }

        [Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(NorthwindLinqToDb.Employee.EmployeeId))]
        public Employee Employee { get; set; }

        [Association(ThisKey = nameof(TerritoryId), OtherKey = nameof(NorthwindLinqToDb.Territory.TerritoryId))]
        public Territory Territory { get; set; }
    }
}
