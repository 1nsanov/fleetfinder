using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Common.Interfaces.Services;

public interface IUserService
{
    public Task<User> GetUserByLoginPassword(string login, string password, CancellationToken cancellationToken);
}