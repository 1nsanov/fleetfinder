using FleetFinder.Application.Common.FeatureModels;

namespace FleetFinder.Application.Features.UserProfile.Get;

public static partial class GetUserProfile
{
    public record ResponseDto(
        string Login,
        FullNameDto FullName,
        string Email,
        string? Organization,
        string? ImageUrl,
        ContactProfileDto Contact
    );
}
