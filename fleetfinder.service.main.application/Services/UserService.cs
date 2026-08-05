using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Services;

public class UserService : IUserService
{
    private readonly QueryDbContext _queryDbContext;
    private readonly IPasswordService _passwordService;

    public UserService(QueryDbContext queryDbContext, IPasswordService passwordService)
    {
        _queryDbContext = queryDbContext;
        _passwordService = passwordService;
    }

    public async Task<User> GetUserByLoginPassword(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _queryDbContext.User.FirstOrDefaultAsync(u => u.Login == login, cancellationToken: cancellationToken);
        if (user is null || !_passwordService.VerifyPassword(password, user.Password))
            throw new EntityNotFoundException($"User with login '{login}' not found");

        return user;
    }
}
