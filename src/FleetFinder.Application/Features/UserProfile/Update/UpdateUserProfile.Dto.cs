using FleetFinder.Application.Common.FeatureModels;

namespace FleetFinder.Application.Features.UserProfile.Update;

public static partial class UpdateUserProfile
{
    public record RequestDto(
        FullNameDto FullName,
        string Email,
        string? Organization,
        string? ImageUrl,
        ContactProfileDto Contact
    );

    public record ResponseDto(bool IsSuccess);
}
