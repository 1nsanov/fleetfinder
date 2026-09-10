using FleetFinder.Application.Features.Identity.GetClaims;
using FleetFinder.Application.Features.Identity.Logout;
using FleetFinder.Application.Features.Identity.Refresh;
using FleetFinder.Application.Features.Identity.SignIn;
using FleetFinder.Application.Features.Identity.SignUp;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Authentication, registration, and token lifecycle.
/// </summary>
[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers a new user and returns JWT tokens.
    /// </summary>
    /// <param name="request">Registration payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Access and refresh tokens.</returns>
    [HttpPost("sign-up")]
    public async Task<SignUp.ResponseDto> SignUp(SignUp.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SignUp.Command(request), cancellationToken);
    }

    /// <summary>
    /// Signs in with login and password and returns JWT tokens.
    /// </summary>
    /// <param name="request">Credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Access and refresh tokens.</returns>
    [HttpPost("sign-in")]
    public async Task<SignIn.ResponseDto> SignIn(SignIn.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SignIn.Command(request), cancellationToken);
    }

    /// <summary>
    /// Issues a new token pair using the current access token and a refresh token header.
    /// </summary>
    /// <param name="refreshToken">Refresh token from the <c>refreshToken</c> header.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Rotated access and refresh tokens.</returns>
    [HttpPost("refresh-token")]
    public async Task<Refresh.ResponseDto> RefreshToken(
        [FromHeader] string refreshToken,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new Refresh.Command(
                HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(), refreshToken),
            cancellationToken);
    }

    /// <summary>
    /// Invalidates the current access token (logout).
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> when logout succeeded.</returns>
    [HttpPost("logout")]
    public async Task<bool> Logout(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new Logout.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()),
            cancellationToken);
    }

    /// <summary>
    /// Returns claims of the current access token and the matching user profile summary.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Token claims and user data.</returns>
    [HttpGet("claims")]
    public async Task<GetClaims.ResponseDto> GetClaims(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetClaims.Query(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()),
            cancellationToken);
    }
}
