using System.Security.Claims;
using FleetFinder.Application.Services.Models;
using FleetFinder.Domain.Users;

namespace FleetFinder.Application.Abstractions.Identity;

public interface IIdentityService
{
    public Task<User> GetUserByAccessToken(string accessToken, CancellationToken cancellationToken);
    public TokenDto GenerateTokenUser(User user);
    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false);
}