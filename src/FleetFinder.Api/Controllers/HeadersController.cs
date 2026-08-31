using System.Security.Claims;

namespace FleetFinder.Api.Controllers;

public abstract class HeadersController : ControllerBase
{
    protected long UserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.Sid);
            return !long.TryParse(value, out var id)
                ? throw new UnauthorizedAccessException("The user ID is missing from the token.")
                : id;
        }
    }
}