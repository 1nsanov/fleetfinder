using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Services;

public class UserService : IUserService
{
    private readonly QueryDbContext _queryDbContext;
    
    public UserService(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task<User> GetUserByLoginPassword(string login, string password, CancellationToken cancellationToken)
    {
        return await _queryDbContext.User.FirstOrDefaultAsync(u => u.Login == login && u.Password == password, cancellationToken: cancellationToken)
               ?? throw new EntityNotFoundException($"User with login '{login}' and password '{password}'");
    }
}