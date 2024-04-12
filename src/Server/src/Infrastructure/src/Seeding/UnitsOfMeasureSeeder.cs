using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal class UnitsOfMeasureSeeder(
    ApplicationDbContext dbContext,
    ILogger<UnitsOfMeasureSeeder> logger
) : SeederBase<UnitOfMeasure>(dbContext, logger)
{
    protected override SeederData RenderSeederData()
    {
        return new SeederData.EnumerableSeederData(
            new UnitOfMeasure[]
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
            }
        );
    }
}