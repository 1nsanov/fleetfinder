namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public sealed class QueryDbContext : BaseDbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
    }
}