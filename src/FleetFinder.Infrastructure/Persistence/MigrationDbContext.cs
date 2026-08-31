namespace FleetFinder.Infrastructure.Persistence;

public sealed class MigrationDbContext : BaseDbContext
{
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
    {
    }
}