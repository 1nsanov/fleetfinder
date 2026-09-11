using FleetFinder.Application.Services.Models;

namespace FleetFinder.Application.Features.Identity.SignIn;

public static partial class SignIn
{
    public record RequestDto(
        string Login,
        string Password
    );
    public record ResponseDto(
        TokenDto Token
    );
}
