namespace FleetFinder.Application.Features.Identity.GetClaims;

public static partial class GetClaims
{
    public record ResponseDto(
        long? Id,
        string? FullName,
        string? ImageUrl
    );
}
