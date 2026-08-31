using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Services;

public class UserService : IUserService
{
    private readonly IQueryDbContext _queryDbContext;
    private readonly IPasswordService _passwordService;

    public UserService(IQueryDbContext queryDbContext, IPasswordService passwordService)
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
