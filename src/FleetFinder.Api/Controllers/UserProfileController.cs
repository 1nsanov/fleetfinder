using FleetFinder.Application.Features.UserProfile.ChangePassword;
using FleetFinder.Application.Features.UserProfile.Get;
using FleetFinder.Application.Features.UserProfile.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Authenticated user profile and password.
/// </summary>
[ApiController]
[Route("api/user-profile")]
public class UserProfileController : HeadersController
{
    private readonly IMediator _mediator;

    public UserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns the profile of the authenticated user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Profile data.</returns>
    [Authorize]
    [HttpGet]
    public async Task<GetUserProfile.ResponseDto> GetUserProfile(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetUserProfile.Query(UserId), cancellationToken);
    }

    /// <summary>
    /// Updates the profile of the authenticated user.
    /// </summary>
    /// <param name="request">Profile payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated profile data.</returns>
    [Authorize]
    [HttpPut]
    public async Task<UpdateUserProfile.ResponseDto> UpdateUserProfile(UpdateUserProfile.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateUserProfile.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Changes the password of the authenticated user.
    /// </summary>
    /// <param name="request">Current and new password.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> when the password was changed.</returns>
    [Authorize]
    [HttpPut("password")]
    public async Task<ChangePassword.ResponseDto> ChangePassword(ChangePassword.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ChangePassword.Command(UserId, request), cancellationToken);
    }
}
