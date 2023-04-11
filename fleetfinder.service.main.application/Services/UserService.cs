using fleetfinder.service.main.domain.Users;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Services;

public class UserService : IUserService
{
    private readonly QueryDbContext _queryDbContext;
    private readonly IConfiguration _config;
    
    public UserService(QueryDbContext queryDbContext, IConfiguration config)
    {
        _queryDbContext = queryDbContext;
        _config = config;
    }


    public Task<User> GetById(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}