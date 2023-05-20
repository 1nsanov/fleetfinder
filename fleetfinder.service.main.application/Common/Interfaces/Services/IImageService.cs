using fleetfinder.service.main.application.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace fleetfinder.service.main.application.Common.Interfaces.Services;

public interface IImageService
{
    public void SaveImage(FirebaseStorageFolder folder, long id, string url, CancellationToken cancellationToken, bool isDelete = false);
}