using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.domain.Users;

public class User : EntityBase
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public RefreshToken? RefreshToken { get; set; } = new();
}

[Owned]
public class RefreshToken
{
    public string? Value { get; set; }
    public DateTime? ExpiryTime { get; set; }
}