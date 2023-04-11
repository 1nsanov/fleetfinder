namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_Logout;

public static partial class UserLogout
{
    public record RequestDto(
        string AccessToken
    );
}