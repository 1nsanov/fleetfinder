namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.User_Get;

public static partial class UserGet
{
    public record ResponseDto(
        long Id,
        string Username
    );
}