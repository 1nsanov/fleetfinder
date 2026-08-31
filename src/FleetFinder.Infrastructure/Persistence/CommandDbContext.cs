using FleetFinder.Application.Abstractions.Persistence;

namespace FleetFinder.Infrastructure.Persistence;

public sealed class CommandDbContext : BaseDbContext, ICommandDbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {
    }
}
