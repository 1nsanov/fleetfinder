namespace FleetFinder.Application.Features.UserProfile.ChangePassword;

public static partial class ChangePassword
{
    public record RequestDto(
        string CurrentPassword,
        string NewPassword
    );

    public record ResponseDto(bool IsSuccess);
}
