using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;

namespace FleetFinder.Application.Features.Images.Delete;

public static partial class DeleteImage
{
    public record Command(RequestDto RequestDto) : ICommandRequest<bool>;

    internal class Handler : IRequestHandler<Command, bool>
    {
        private readonly IObjectStorageService _objectStorage;

        public Handler(IObjectStorageService objectStorage)
        {
            _objectStorage = objectStorage;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            foreach (var url in requestDto.Urls)
            {
                try
                {
                    var fileName = url
                        .Split("/").Last()
                        .Split("?").First();

                    await _objectStorage.DeleteAsync(requestDto.Folder, fileName, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred: {ex.Message}");
                }
            }

            return true;
        }
    }
}
