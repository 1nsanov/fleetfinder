using fleetfinder.service.main.application.Common.Interfaces.Persistence;

namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public sealed class CommandDbContext : BaseDbContext, ICommandDbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {
    }
}
