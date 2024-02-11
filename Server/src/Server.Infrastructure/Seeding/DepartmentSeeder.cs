namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface IDepartmentSeeder : ISeeder
{
}

internal class DepartmentSeeder : IDepartmentSeeder
{
    private readonly ApplicationDbContext _dbContext;

    public DepartmentSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        var departments = new List<Department>
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
        };

        if (_dbContext.Departments.Any())
            return;

        await _dbContext.Departments.AddRangeAsync(departments);
        await _dbContext.SaveChangesAsync();
    }
}