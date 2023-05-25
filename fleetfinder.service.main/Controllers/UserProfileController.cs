using fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_PassswordPut;
using fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_Put;
using fleetfinder.service.main.application.Features.UserProfileFeatures.Query.UserProfile_Get;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

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
    public async Task<UserProfileGet.ResponseDto> UserProfileGet(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UserProfileGet.Query(UserId), cancellationToken);
    } 
    
    [Authorize]
    [HttpPut]
    public async Task<UserProfilePut.ResponseDto> UserProfilePut(UserProfilePut.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UserProfilePut.Command(UserId, request), cancellationToken);
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<UserProfilePasswordPut.ResponseDto> UserProfilePasswordPut(UserProfilePasswordPut.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UserProfilePasswordPut.Command(UserId, request), cancellationToken);
    }
}