using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FleetFinder.Application.Abstractions.Persistence;

public interface ICommandDbContext : IQueryDbContext
{
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
