namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface IUnitsOfMeasureSeeder : ISeeder
{
}

internal class UnitsOfMeasureSeeder : IUnitsOfMeasureSeeder
{
    private readonly ApplicationDbContext _dbContext;

    public UnitsOfMeasureSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (_dbContext.UnitsOfMeasure.Any())
            return;

        var unitsOfMeasure = new List<UnitOfMeasure>
        {
            new() { Name = "Gram", Symbol = "g" },
            new() { Name = "Pound", Symbol = "lb" },
            new() { Name = "ounce", Symbol = "oz" },
            new() { Name = "Fluid Ounce", Symbol = "fl oz" },
            new() { Name = "Pint", Symbol = "pt" },
            new() { Name = "Quart", Symbol = "qt" },
            new() { Name = "Gallon", Symbol = "gal" },
            new() { Name = "Milliliter", Symbol = "ml" },
            new() { Name = "Liter", Symbol = "l" },
            new() { Name = "Count", Symbol = "ct" }
        };

        await _dbContext.UnitsOfMeasure.AddRangeAsync(unitsOfMeasure);
        await _dbContext.SaveChangesAsync();
    }
}