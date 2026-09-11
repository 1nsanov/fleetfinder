namespace FleetFinder.Infrastructure.Persistence;

public sealed class QueryDbContext : BaseDbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
    }
}
