using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Application.Features.Images;

namespace FleetFinder.Application.Features.Images.Upload;

public static partial class UploadImage
{
    public record Command(RequestDto RequestDto) : ICommandRequest<List<string>>;

    internal class Handler : IRequestHandler<Command, List<string>>
    {
        private readonly IObjectStorageService _objectStorage;

        public Handler(IObjectStorageService objectStorage)
        {
            _objectStorage = objectStorage;
        }

        public async Task<List<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            if (requestDto.Files.Count == 0) return new List<string>();
            if (requestDto.Files.Count > ImageUploadLimits.MaxFileCount)
                throw new ArgumentException($"Cannot upload more than {ImageUploadLimits.MaxFileCount} files.");

            foreach (var dto in requestDto.Files)
            {
                if (dto.Length <= 0)
                    throw new ArgumentException("Empty file.");
                if (dto.Length > ImageUploadLimits.MaxFileBytes)
                    throw new ArgumentException($"File size must not exceed {ImageUploadLimits.MaxFileBytes / (1024 * 1024)} MB.");
                if (!ImageUploadLimits.IsAllowed(dto.FileName, dto.ContentType))
                    throw new ArgumentException("Only JPEG and PNG images are allowed.");
            }

            var response = new List<string>();

            foreach (var item in requestDto.Files)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                var imageUrl = await _objectStorage.UploadAsync(
                    requestDto.Folder,
                    fileName,
                    item.Stream,
                    item.ContentType,
                    cancellationToken);
                response.Add(imageUrl);
            }

            return response;
        }
    }
}
