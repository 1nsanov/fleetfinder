using System.ComponentModel.DataAnnotations;
using fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_Logout;
using fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_RefreshToken;
using fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_SignIn;
using fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_SignUp;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers
{
    [ApiController]
    [Route("api/identify")]
    public class IdentifyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentifyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IdentifySignUp.ResponseDto> UserSignUp(IdentifySignUp.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new IdentifySignUp.Command(request), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IdentifySignIn.ResponseDto> UserGetSignIn(IdentifySignIn.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new IdentifySignIn.Command(request), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("refreshToken")]
        public async Task<IdentifyRefreshToken.ResponseDto> UserRefreshToken([FromHeader][Required] string refreshToken,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new IdentifyRefreshToken.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(), refreshToken), cancellationToken);
        }
 
        [Authorize]
        [HttpPost("logout")]
        public async Task<bool> UserLogout(CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new IdentifyLogout.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()), cancellationToken);
        }
    }
}