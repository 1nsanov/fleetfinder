using Firebase.Storage;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;

public static partial class ImagePost
{

    public record Command(RequestDto RequestDto) : ICommandRequest<string>;
    
    internal class Handler : IRequestHandler<Command, string>
    {
        private readonly IConfiguration _config;
        private readonly IImageService _imageService;
        
        public Handler(IConfiguration config, IImageService imageService)
        {
            _config = config;
            _imageService = imageService;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;
            if (requestDto.File.Length <= 0)
                throw new Exception("Invalid file.");
        
            long maxFileSize = 10 * 1024 * 1024; // 10 MB
            if (requestDto.File.Length > maxFileSize)
                throw new Exception("File size exceeds the maximum allowed limit.");

            try
            {
                var stream = requestDto.File.OpenReadStream();
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(requestDto.File.FileName)}";
                var firebaseStorage = new FirebaseStorage(_config["FirebaseStorage:Bucket"]);
                var imageUrl = await firebaseStorage
                    .Child(_config[$"FirebaseStorage:Folders:{requestDto.Folder.ToString()}"])
                    .Child(fileName)
                    .PutAsync(stream);

                _imageService.SaveImage(requestDto.Folder, requestDto.Id, imageUrl, cancellationToken);
            
                return imageUrl;
            }
            catch (Exception ex)
            {
                throw new Exception( $"An error occurred: {ex.Message}");
            }
        }
    }
}