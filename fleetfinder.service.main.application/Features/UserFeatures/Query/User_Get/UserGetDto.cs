namespace fleetfinder.service.main.application.Features.UserFeatures.Query.User_Get;

public static partial class UserGet
{
    public record ResponseDto(
        long Id,
        string Username
    );
    
    // public class ResponseDto
    // {
    //     public long Id { get; init; }
    //     public string Username { get; init; }
    // }
}