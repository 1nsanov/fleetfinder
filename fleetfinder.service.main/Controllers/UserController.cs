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
        [HttpPost("SignUp")]
        public async Task<UserSignUp.ResponseDto> UserSignUp(UserSignUp.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserSignUp.Command(request), cancellationToken);
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