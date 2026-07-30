using Amazon.S3;
using Amazon.S3.Model;
using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using Microsoft.Extensions.Options;

namespace fleetfinder.service.main.application.Services;

public class S3ObjectStorageService : IObjectStorageService
{
    private readonly IAmazonS3 _s3;
    private readonly S3StorageOptions _options;

    public S3ObjectStorageService(IAmazonS3 s3, IOptions<S3StorageOptions> options)
    {
        _s3 = s3;
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<string> UploadAsync(
        StorageFolder folder,
        string fileName,
        Stream stream,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var key = BuildKey(folder, fileName);
        var request = new PutObjectRequest
        {
            BucketName = _options.Bucket,
            Key = key,
            InputStream = stream,
            ContentType = contentType
        };

        await _s3.PutObjectAsync(request, cancellationToken);
        return BuildPublicUrl(key);
    }

    public async Task DeleteAsync(StorageFolder folder, string fileName, CancellationToken cancellationToken = default)
    {
        var key = BuildKey(folder, fileName);
        await _s3.DeleteObjectAsync(_options.Bucket, key, cancellationToken);
    }

    private string BuildKey(StorageFolder folder, string fileName)
    {
        if (!_options.Folders.TryGetValue(folder.ToString(), out var folderPath) ||
            string.IsNullOrWhiteSpace(folderPath))
        {
            throw new InvalidOperationException($"S3Storage folder '{folder}' is not configured");
        }

        return $"{folderPath.Trim('/')}/{fileName}";
    }

    private string BuildPublicUrl(string key)
    {
        var baseUrl = _options.PublicBaseUrl.TrimEnd('/');
        return $"{baseUrl}/{_options.Bucket}/{key}";
    }
}
