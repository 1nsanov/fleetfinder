using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;
using fleetfinder.service.main.application.Features.ImageFeatures.Command.ImageDelete;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/image")]
// [Authorize]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<List<string>> UploadImage([FromForm] ImagePost.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ImagePost.Command(request), cancellationToken);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteImage(
        [FromQuery] FirebaseStorageFolder folder,
        [FromQuery] List<string> url,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ImageDelete.Command(new ImageDelete.RequestDto(folder, url)), cancellationToken);
        return Ok();
    }
}