namespace fleetfinder.service.main.application.Services.UserService.Models;

public class TokenDto
{
    public string Access { get; set; } = null!;
    public string Refresh { get; set; } = null!;
    public DateTime Expiration { get; set; }
}