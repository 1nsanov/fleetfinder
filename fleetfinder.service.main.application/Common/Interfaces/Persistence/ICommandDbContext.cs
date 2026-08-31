using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace fleetfinder.service.main.application.Common.Interfaces.Persistence;

public interface ICommandDbContext : IQueryDbContext
{
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
