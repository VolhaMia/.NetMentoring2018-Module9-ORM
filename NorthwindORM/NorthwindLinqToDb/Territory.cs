using LinqToDB.Mapping;

namespace NorthwindLinqToDb
{
    [Table("[dbo].[Territories]")]
    public class Territory
    {
        [Column("TerritoryID")]
        [PrimaryKey]
        [Identity]
        public int TerritoryId { get; set; }

        [Column("TerritoryDescription")]
        [NotNull]
        public string TerritoryDescription { get; set; }

        [Column("RegionID")]
        [NotNull]
        public int RegionId { get; set; }

        [Association(ThisKey = nameof(RegionId), OtherKey = nameof(NorthwindLinqToDb.Region.RegionId))]
        public Region Region { get; set; }
    }
}
