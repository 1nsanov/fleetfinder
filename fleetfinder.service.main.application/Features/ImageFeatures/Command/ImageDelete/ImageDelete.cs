using Firebase.Storage;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.ImageDelete;

public static partial class ImageDelete
{

    public record Command(RequestDto RequestDto) : ICommandRequest<bool>;
    
    internal class Handler : IRequestHandler<Command, bool>
    {
        private readonly IConfiguration _config;
        private readonly IImageService _imageService;
        
        public Handler(IConfiguration config, IImageService imageService)
        {
            _config = config;
            _imageService = imageService;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;
            
            try
            {
                var firebaseStorage = new FirebaseStorage(_config["FirebaseStorage:Bucket"]);
           
                var fileName = requestDto.Url
                    .Split("/").Last()
                    .Split("?").First()
                    .Split("%").Last()[2..];
            
                await firebaseStorage
                    .Child(_config[$"FirebaseStorage:Folders:{requestDto.Folder.ToString()}"])
                    .Child(fileName)
                    .DeleteAsync();
            
                _imageService.SaveImage(requestDto.Folder, requestDto.Id, requestDto.Url, cancellationToken, true);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception( $"An error occurred: {ex.Message}");
            }
        }
    }
}