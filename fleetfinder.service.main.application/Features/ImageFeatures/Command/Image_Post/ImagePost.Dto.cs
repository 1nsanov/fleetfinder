using fleetfinder.service.main.application.Common.Enums;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;

public static partial class ImagePost
{
    public record FileUpload(string FileName, string ContentType, long Length, Stream Stream);

    public record RequestDto(
        StorageFolder Folder,
        List<FileUpload> Files
    );
}
