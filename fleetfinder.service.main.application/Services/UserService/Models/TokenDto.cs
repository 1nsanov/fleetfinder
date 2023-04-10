namespace fleetfinder.service.main.application.Services.UserService.Models;

public class TokenDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
}