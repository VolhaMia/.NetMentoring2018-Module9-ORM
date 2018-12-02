namespace NorthwindEF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NorthwindEF.NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NorthwindEF.NorthwindContext context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "MyCategory1" },
                new Category { CategoryName = "MyCategory2" });

            context.Regions.AddOrUpdate(r => r.RegionID,
                new Region { RegionDescription = "MyRegion1", RegionID = 11 });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory { TerritoryID = "111111", TerritoryDescription = "MyTerritoryDescription1", RegionID = 11 },
                new Territory { TerritoryID = "222222", TerritoryDescription = "MyTerritoryDescription2", RegionID = 11 });
        }
    }
}
