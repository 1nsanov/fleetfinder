using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Common.Interfaces;

public interface IUserService
{
    public Task<User> GetById(long id, CancellationToken cancellationToken);
}