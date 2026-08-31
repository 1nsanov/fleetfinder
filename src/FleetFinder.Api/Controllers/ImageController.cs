using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.Images.Delete;
using FleetFinder.Application.Features.Images.Upload;

namespace FleetFinder.Api.Controllers;

[ApiController]
[Route("api/image")]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<List<string>> UploadImage(
        [FromForm] StorageFolder folder,
        [FromForm] List<IFormFile> files,
        CancellationToken cancellationToken)
    {
        var request = new UploadImage.RequestDto(
            folder,
            files.Select(file => new UploadImage.FileUpload(
                file.FileName,
                file.ContentType,
                file.Length,
                file.OpenReadStream())).ToList());
        return await _mediator.Send(new UploadImage.Command(request), cancellationToken);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage(
        [FromQuery] StorageFolder folder,
        [FromQuery] List<string> url,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteImage.Command(new DeleteImage.RequestDto(folder, url)), cancellationToken);
        return Ok();
    }
}
