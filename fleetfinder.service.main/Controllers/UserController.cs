using fleetfinder.service.main.application.Features.UserFeatures.Command.User_PostSignIn;
using fleetfinder.service.main.application.Features.UserFeatures.Command.User_PostSignUp;
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
        public async Task<UserPostSignUp.ResponseDto> UserSignUp(UserPostSignUp.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserPostSignUp.Command(request), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<UserPostSignIn.ResponseDto> UserGetSignIn(UserPostSignIn.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserPostSignIn.Query(request), cancellationToken);
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