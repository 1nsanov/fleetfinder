namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.Identify_GetClaims;

public static partial class IdentifyGetClaims
{
    public record ResponseDto(
        long? Id,
        string? FullName,
        string? ImageUrl
    );
}