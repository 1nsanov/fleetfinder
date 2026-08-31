using FleetFinder.Application.Features.Identity.GetClaims;
using FleetFinder.Application.Features.Identity.Logout;
using FleetFinder.Application.Features.Identity.Refresh;
using FleetFinder.Application.Features.Identity.SignIn;
using FleetFinder.Application.Features.Identity.SignUp;

namespace FleetFinder.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    public async Task<SignUp.ResponseDto> SignUp(SignUp.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SignUp.Command(request), cancellationToken);
    }

    [HttpPost("sign-in")]
    public async Task<SignIn.ResponseDto> SignIn(SignIn.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SignIn.Command(request), cancellationToken);
    }

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

    [HttpPost("logout")]
    public async Task<bool> Logout(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new Logout.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()),
            cancellationToken);
    }

    [HttpGet("claims")]
    public async Task<GetClaims.ResponseDto> GetClaims(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetClaims.Query(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()),
            cancellationToken);
    }
}
