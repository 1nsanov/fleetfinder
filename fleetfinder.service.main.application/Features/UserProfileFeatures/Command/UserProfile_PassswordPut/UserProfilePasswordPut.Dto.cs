namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_PassswordPut;

public static partial class UserProfilePasswordPut
{
    public record RequestDto(
        string CurrentPassword,
        string NewPassword
    );

    public record ResponseDto(bool IsSuccess);
}