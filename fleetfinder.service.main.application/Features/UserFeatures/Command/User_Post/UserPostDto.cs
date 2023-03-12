namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_Post;

public static partial class UserPost
{
    public record RequestDto(string Login);

    public record ResponseDto(long Id);
}