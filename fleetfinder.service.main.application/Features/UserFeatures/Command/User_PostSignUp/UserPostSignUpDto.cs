using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_PostSignUp;

public static partial class UserPostSignUp
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