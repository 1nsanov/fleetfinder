namespace fleetfinder.service.main.application.Services;

public class TokenService
{
    private readonly HashSet<string> _revokedTokens = new();

    public void RevokeToken(string tokenId)
    {
        _revokedTokens.Add(tokenId);
    }

    public bool IsTokenRevoked(string tokenId)
    {
        return _revokedTokens.Contains(tokenId);
    }
}