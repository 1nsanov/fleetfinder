using System.ComponentModel.DataAnnotations;
using fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_GetClaims;
using fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_Logout;
using fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_RefreshToken;
using fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignIn;
using fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;
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
        
        [HttpPost("sign-up")]
        public async Task<IdentifySignUp.ResponseDto> IdentifySignUp(IdentifySignUp.RequestDto request,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new IdentifySignUp.Command(request), cancellationToken);
        }
        
        [HttpPost("sign-in")]
        public async Task<IdentifySignIn.ResponseDto> IdentifySignIn(IdentifySignIn.RequestDto request,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new IdentifySignIn.Command(request), cancellationToken);
        }
        
        [HttpPost("refresh-token")]
        public async Task<IdentifyRefreshToken.ResponseDto> IdentifyRefreshToken(
            [FromHeader] string refreshToken,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new IdentifyRefreshToken.Command(
                    HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(), refreshToken),
                cancellationToken);
        }

        [HttpGet("logout")]
        public async Task<bool> IdentifyLogout(CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new IdentifyLogout.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")
                    .Last()), cancellationToken);
        }
        
        [HttpGet("claims")]
        public async Task<IdentifyGetClaims.ResponseDto> IdentifyGetClaims(CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new IdentifyGetClaims.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()), cancellationToken);
        }

        #region Test

        [Authorize]
        [HttpGet("test/auth")]
        public void Test()
        {
            Ok();
        }

        #endregion
    }
}