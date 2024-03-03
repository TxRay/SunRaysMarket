using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal class DepartmentSeeder(ApplicationDbContext dbContext, ILogger<SeederBase<Department>> logger)
    : SeederBase<Department>(dbContext, logger)
{
    protected override SeederData RenderSeederData()
        => new SeederData.EnumerableSeederData(
            new Department[]
            {
                new()
                {
                    Name = "Bakery",
                    Slug = Slugs.CreateSlug("Bakery"),
                    Description = "Bakery department."
                },
                new()
                {
                    Name = "Dairy & Eggs",
                    Slug = Slugs.CreateSlug("Dairy & Eggs"),
                    Description = "Dairy and eggs department ."
                },
                new()
                {
                    Name = "Frozen Foods",
                    Slug = Slugs.CreateSlug("Frozen Foods"),
                    Description = "Frozen foods."
                },
                new()
                {
                    Name = "Meat & Seafood",
                    Slug = Slugs.CreateSlug("Meat & Seafood"),
                    Description = "Meat and seafood."
                },
                new()
                {
                    Name = "Produce",
                    Slug = Slugs.CreateSlug("Produce"),
                    Description = "Produce."
                }
            }
        );
}