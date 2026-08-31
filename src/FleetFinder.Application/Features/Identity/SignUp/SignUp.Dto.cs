using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Application.Services.Models;

namespace FleetFinder.Application.Features.Identity.SignUp;

public static partial class SignUp
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
