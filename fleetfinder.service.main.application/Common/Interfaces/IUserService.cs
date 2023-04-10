using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using fleetfinder.service.main.application.Services.UserService.Models;
using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Common.Interfaces;

public interface IUserService
{
    public Task<User> GetUserByAccessToken(string accessToken, CancellationToken cancellationToken);
    public TokenDto GenerateTokenUser(User user);
    public JwtSecurityToken GenerateToken(User user);
    public Task<User> Authenticate(string login, string password, CancellationToken cancellationToken);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
}