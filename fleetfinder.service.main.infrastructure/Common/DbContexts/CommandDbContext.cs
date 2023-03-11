namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public sealed class CommandDbContext : BaseDbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {
    }
}