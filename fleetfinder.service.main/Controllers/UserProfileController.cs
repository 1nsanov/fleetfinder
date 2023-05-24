using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/user-profile")]
[Authorize]
public class UserProfileController : HeadersController
{
    private readonly IMediator _mediator;

    public UserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
}