using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_PostSignIn;

public static partial class UserPostSignIn
{
    public record RequestDto(
        string Login,
        string Password
    );
    public record ResponseDto(
        TokenDto Token
    );
}