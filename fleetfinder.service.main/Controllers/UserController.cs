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
        [HttpPost("SignUp")]
        public async Task<UserPostSignUp.ResponseDto> UserSignUp(UserPostSignUp.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserPostSignUp.Command(request), cancellationToken);
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