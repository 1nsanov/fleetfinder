namespace fleetfinder.service.main.application.Services.Models;

public class TokenDto
{
    public string Access { get; set; } = null!;
    public string Refresh { get; set; } = null!;
    public DateTime ExpiryTime { get; set; } 
}