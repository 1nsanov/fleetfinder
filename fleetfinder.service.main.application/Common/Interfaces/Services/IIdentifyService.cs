using System.Security.Claims;
using fleetfinder.service.main.application.Services.Models;
using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Common.Interfaces.Services;

public interface IIdentifyService
{
    public Task<User> GetUserByAccessToken(string accessToken, CancellationToken cancellationToken);
    public TokenDto GenerateTokenUser(User user);
    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false);
}