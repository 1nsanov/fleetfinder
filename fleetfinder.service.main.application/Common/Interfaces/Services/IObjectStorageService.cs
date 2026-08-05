using fleetfinder.service.main.application.Common.Enums;

namespace fleetfinder.service.main.application.Common.Interfaces.Services;

public interface IObjectStorageService
{
    Task<string> UploadAsync(StorageFolder folder, string fileName, Stream stream, string contentType, CancellationToken cancellationToken = default);
    Task DeleteAsync(StorageFolder folder, string fileName, CancellationToken cancellationToken = default);
}
