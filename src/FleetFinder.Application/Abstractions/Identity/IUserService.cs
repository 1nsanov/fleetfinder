using FleetFinder.Domain.Users;

namespace FleetFinder.Application.Abstractions.Identity;

public interface IUserService
{
    public Task<User> GetUserByLoginPassword(string login, string password, CancellationToken cancellationToken);
}