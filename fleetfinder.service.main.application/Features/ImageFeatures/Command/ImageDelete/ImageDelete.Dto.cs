using fleetfinder.service.main.application.Common.Enums;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.ImageDelete;

public static partial class ImageDelete
{
    public record RequestDto(
        StorageFolder Folder, 
        List<string> Urls
    );
}