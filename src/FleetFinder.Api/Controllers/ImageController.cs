using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.Images.Delete;
using FleetFinder.Application.Features.Images.Upload;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Object-storage uploads and deletes for listing and profile images.
/// </summary>
[Authorize]
[ApiController]
[Route("api/image")]
public class ImageController : HeadersController
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Uploads one or more image files to the given storage folder.
    /// </summary>
    /// <param name="folder">Target storage folder.</param>
    /// <param name="files">Image files.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Public URLs of uploaded images.</returns>
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

    /// <summary>
    /// Deletes images from the given storage folder by URL.
    /// </summary>
    /// <param name="folder">Storage folder.</param>
    /// <param name="url">Image URLs to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
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
