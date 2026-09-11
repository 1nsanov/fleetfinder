using FleetFinder.Application.Common.Enums;

namespace FleetFinder.Application.Features.Images.Upload;

public static partial class UploadImage
{
    public record FileUpload(string FileName, string ContentType, long Length, Stream Stream);

    public record RequestDto(
        StorageFolder Folder,
        List<FileUpload> Files
    );
}
