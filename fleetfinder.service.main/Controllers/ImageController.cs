using fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;
using fleetfinder.service.main.application.Features.ImageFeatures.Command.ImageDelete;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/image")]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<string> UploadImage(ImagePost.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ImagePost.Command(request), cancellationToken);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage(ImageDelete.RequestDto request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ImageDelete.Command(request), cancellationToken);
        return Ok();
    }
}