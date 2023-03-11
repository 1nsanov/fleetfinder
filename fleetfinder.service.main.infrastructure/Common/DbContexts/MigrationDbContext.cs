namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public sealed class MigrationDbContext : BaseDbContext
{
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
    {
    }
}