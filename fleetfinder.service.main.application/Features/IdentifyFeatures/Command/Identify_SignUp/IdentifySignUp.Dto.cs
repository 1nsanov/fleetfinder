using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.application.Services.Models;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record RequestDto(
        string Login,
        string Password,
        string Email,
        FullNameDto FullName,
        string? Organization
    );
    
    public record ResponseDto(
        TokenDto Token
    );
}