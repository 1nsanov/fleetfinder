using fleetfinder.service.main.application.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;

public static partial class ImagePost
{
    public record RequestDto(
        FirebaseStorageFolder Folder, 
        long Id, 
        IFormFile File
    );
}