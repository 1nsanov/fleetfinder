using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_RefreshToken;

public static partial class UserRefreshToken
{
    public record ResponseDto(
        TokenDto Token
    );
}