using fleetfinder.service.main.application.Common.FeatureModels;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_Put;

public static partial class UserProfilePut
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