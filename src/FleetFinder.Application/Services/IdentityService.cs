using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Application.Common.Options;
using FleetFinder.Application.Services.Models;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FleetFinder.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly IQueryDbContext _queryDbContext;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(IQueryDbContext queryDbContext, IOptions<JwtOptions> jwtOptions)
    {
        _queryDbContext = queryDbContext;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<User> GetUserByAccessToken(string accessToken, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromToken(accessToken) ?? throw new Exception("Invalid access token or refresh token");

        var claim = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("nameidentifier"));
        var login = claim?.Value;

        return await _queryDbContext.User.FirstOrDefaultAsync(u => u.Login == login, cancellationToken)
            ?? throw new EntityNotFoundException("User by access token not found");
    }

    public TokenDto GenerateTokenUser(User user)
    {
        var token = GenerateJwtSecurityToken(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays);

        user.RefreshToken = new RefreshToken
        {
            Value = refreshToken,
            ExpiryTime = refreshExpiry
        };

        return new TokenDto
        {
            Access = accessToken,
            Refresh = refreshToken,
            ExpiryTime = token.ValidTo
        };
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
            ValidateLifetime = validateLifetime,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    #region Private

    private JwtSecurityToken GenerateJwtSecurityToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, $"{user.FullName.First} {user.FullName.Second} {user.FullName.Surname}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return new JwtSecurityToken(_jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes),
            signingCredentials: credentials);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    #endregion
}