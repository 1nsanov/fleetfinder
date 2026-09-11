using FleetFinder.Application.Services.Models;

namespace FleetFinder.Application.Features.Identity.Refresh;

public static partial class Refresh
{
    public record ResponseDto(
        TokenDto? Token
    );
}
