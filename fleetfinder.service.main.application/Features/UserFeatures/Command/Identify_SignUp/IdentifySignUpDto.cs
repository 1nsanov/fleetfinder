using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record RequestDto(
        string Login,
        string Password,
        string Email,
        Name Name
    );

    public record Name(
        string Last,
        string First,
        string Middle
    );
    
    public record ResponseDto(
        TokenDto Token
    );
}