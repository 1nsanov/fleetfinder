using FleetFinder.Application.Common.Enums;

namespace FleetFinder.Application.Features.Images.Delete;

public static partial class DeleteImage
{
    public record RequestDto(
        StorageFolder Folder, 
        List<string> Urls
    );
}
