using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Services.Models;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace fleetfinder.service.main.application.Services;

public class IdentifyService : IIdentifyService
{
    private readonly QueryDbContext _queryDbContext;
    private readonly CommandDbContext _commandDbContext;
    private readonly IConfiguration _config;

    public IdentifyService(QueryDbContext queryDbContext, IConfiguration config, CommandDbContext commandDbContext)
    {
        _queryDbContext = queryDbContext;
        _config = config;
        _commandDbContext = commandDbContext;
    }

    public async Task<User> GetUserByAccessToken(string accessToken, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken) ?? throw new Exception("Invalid access token or refresh token");

        var claim = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("nameidentifier"));
        var login = claim?.Value;

        return await _queryDbContext.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken)
            ?? throw new EntityNotFoundException("User by access token not found");
    }
    
    public TokenDto GenerateTokenUser(User user)
    {
        var token = GenerateJwtSecurityToken(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = GenerateRefreshToken();
        
        user.RefreshToken.Value = refreshToken;
        user.RefreshToken.ExpiryTime = token.ValidTo;
        
        return new TokenDto
        {
            Access = accessToken,
            Refresh = refreshToken,
            Expiration = token.ValidTo
        };
    }

   
    #region Private

    private JwtSecurityToken GenerateJwtSecurityToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FullName),
            // new Claim(ClaimTypes.Role, role.Code)
        };

        return new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
    }
    
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
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