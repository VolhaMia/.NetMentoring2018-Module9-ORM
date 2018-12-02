using LinqToDB.Mapping;

namespace NorthwindLinqToDb
{
    [Table("[dbo].[Region]")]
    public class Region
    {
        [Column("RegionID")]
        [Identity]
        [PrimaryKey]
        public int RegionId { get; set; }

        [Column("RegionDescription")]
        [NotNull]
        public string RegionDescription { get; set; }
    }
}
