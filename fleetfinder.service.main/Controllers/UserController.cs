using fleetfinder.service.main.application.Features.UserFeatures.Command.User_Post;
using fleetfinder.service.main.application.Features.UserFeatures.Query.User_Get;

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

        [HttpGet("base")]
        public async Task<UserGet.ResponseDto> GetBase([FromQuery] long id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserGet.Query(id), cancellationToken);
        }
        
        [HttpPost("base")]
        public async Task<UserPost.ResponseDto> PostBase([FromBody] UserPost.RequestDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserPost.Command(request), cancellationToken);
        }
    }
}