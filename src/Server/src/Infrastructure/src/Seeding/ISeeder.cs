namespace SunRaysMarket.Server.Infrastructure.Seeding;

internal interface ISeeder
{
    public Task SeedAsync();
    public bool ShouldSeed();
}