using fleetfinder.service.main.application.Features.UserFeatures.Command.User_Logout;
using fleetfinder.service.main.application.Features.UserFeatures.Command.User_SignIn;
using fleetfinder.service.main.application.Features.UserFeatures.Command.User_SignUp;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<UserSignUp.ResponseDto> UserSignUp(UserSignUp.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserSignUp.Command(request), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<UserSignIn.ResponseDto> UserGetSignIn(UserSignIn.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserSignIn.Command(request), cancellationToken);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<bool> UserLogout(CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new UserLogout.Command(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()), cancellationToken);
        }

        #region Test

        [Authorize]
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("You is login!");
        }

        #endregion
    }
}