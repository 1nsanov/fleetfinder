using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record RequestDto(
        string Login,
        string Password,
        string Email,
        FullName FullName,
        string? Organization
    );

    public record FullName(
        string First,
        string Second,
        string? Surname
    );
    
    public record ResponseDto(
        TokenDto Token
    );
}