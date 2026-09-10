using System.Security.Claims;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Base controller that reads the authenticated user id from JWT claims.
/// </summary>
public abstract class HeadersController : ControllerBase
{
    /// <summary>
    /// User id from <see cref="ClaimTypes.Sid"/> in the access token.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">The token does not contain a user id.</exception>
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
