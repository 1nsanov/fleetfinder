using FleetFinder.Application.Features.UserProfile.ChangePassword;
using FleetFinder.Application.Features.UserProfile.Get;
using FleetFinder.Application.Features.UserProfile.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

[ApiController]
[Route("api/user-profile")]
public class UserProfileController : HeadersController
{
    private readonly IMediator _mediator;

    public UserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<GetUserProfile.ResponseDto> GetUserProfile(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetUserProfile.Query(UserId), cancellationToken);
    }

    [Authorize]
    [HttpPut]
    public async Task<UpdateUserProfile.ResponseDto> UpdateUserProfile(UpdateUserProfile.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateUserProfile.Command(UserId, request), cancellationToken);
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<ChangePassword.ResponseDto> ChangePassword(ChangePassword.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ChangePassword.Command(UserId, request), cancellationToken);
    }
}
