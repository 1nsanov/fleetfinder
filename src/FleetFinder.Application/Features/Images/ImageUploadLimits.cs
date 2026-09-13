namespace FleetFinder.Application.Features.Images;

public static class ImageUploadLimits
{
    public const int MaxFileCount = 5;
    public const long MaxFileBytes = 10 * 1024 * 1024;

    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/jpg",
        "image/png"
    };

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png"
    };

    public static bool IsAllowed(string fileName, string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return false;

        var extension = Path.GetExtension(fileName);
        return AllowedContentTypes.Contains(contentType) && AllowedExtensions.Contains(extension);
    }
}
