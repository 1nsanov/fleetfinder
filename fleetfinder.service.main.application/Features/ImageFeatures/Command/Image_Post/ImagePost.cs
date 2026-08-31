using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;

public static partial class ImagePost
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

            foreach (var dto in requestDto.Files)
            {
                if (dto.Length <= 0) throw new Exception("Invalid file.");
            }

            var response = new List<string>();

            foreach (var item in requestDto.Files)
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                    var contentType = string.IsNullOrWhiteSpace(item.ContentType)
                        ? "application/octet-stream"
                        : item.ContentType;
                    var imageUrl = await _objectStorage.UploadAsync(
                        requestDto.Folder,
                        fileName,
                        item.Stream,
                        contentType,
                        cancellationToken);
                    response.Add(imageUrl);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred: {ex.Message}");
                }
            }

            return response;
        }
    }
}
