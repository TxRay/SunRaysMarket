using Microsoft.Extensions.Logging;

namespace SunRaysMarket.Server.Infrastructure.Seeding;

#nullable disable

internal abstract class SeederBase<TEntity> : ISeeder
    where TEntity : class
{
    private readonly ILogger _logger;

    protected SeederBase(ApplicationDbContext dbContext, ILogger logger)
    {
        DbContext = dbContext;
        _logger = logger;
    }

    protected ApplicationDbContext DbContext { get; }

    public async Task SeedAsync()
    {
        if (!ShouldSeed())
            return;

        var seederData = RenderSeederData();
        var dbSet = DbContext.Set<TEntity>();

        try
        {
            switch (seederData)
            {
                case SeederData.EntitySeederData entitySeederData:
                    await dbSet.AddAsync(entitySeederData.Data);
                    break;
                case SeederData.EnumerableSeederData enumerableSeederData:
                    await dbSet.AddRangeAsync(enumerableSeederData.Data);
                    break;
            }

            await DbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError(
                "Data seeding for the entity '{ent}' failed.",
                typeof(TEntity).FullName
            );
            throw;
        }
    }

    public virtual bool ShouldSeed()
    {
        return !DbContext.Set<TEntity>().Any();
    }

    protected abstract SeederData RenderSeederData();

    protected abstract record SeederData
    {
        public abstract record GenericSeederData<TData>(TData Data) : SeederData;

        public record EntitySeederData(TEntity Data) : GenericSeederData<TEntity>(Data);

        public record EnumerableSeederData(IEnumerable<TEntity> Data)
            : GenericSeederData<IEnumerable<TEntity>>(Data);
    }
}
