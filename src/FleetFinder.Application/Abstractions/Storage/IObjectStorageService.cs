using FleetFinder.Application.Common.Enums;

namespace FleetFinder.Application.Abstractions.Storage;

public interface IObjectStorageService
{
    Task<string> UploadAsync(StorageFolder folder, string fileName, Stream stream, string contentType, CancellationToken cancellationToken = default);
    Task DeleteAsync(StorageFolder folder, string fileName, CancellationToken cancellationToken = default);
}
