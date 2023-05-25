using fleetfinder.service.main.application.Common.FeatureModels;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Query.UserProfile_Get;

public static partial class UserProfileGet
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