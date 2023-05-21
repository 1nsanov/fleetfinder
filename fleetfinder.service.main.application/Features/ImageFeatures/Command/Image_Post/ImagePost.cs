using Firebase.Storage;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Features.ImageFeatures.Command.Image_Post;

public static partial class ImagePost
{

    public record Command(RequestDto RequestDto) : ICommandRequest<List<string>>;
    
    internal class Handler : IRequestHandler<Command, List<string>>
    {
        private readonly IConfiguration _config;
        
        public Handler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            if (requestDto.Files.Count == 0) return new List<string>();
            
            requestDto.Files.ForEach(dto =>
            {
                if (dto.Length <= 0) throw new Exception("Invalid file.");
            });

            var response = new List<string>();

            foreach (var item in requestDto.Files)
            {
                try
                {
                    var stream = item.OpenReadStream();
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item.FileName)}";
                    var firebaseStorage = new FirebaseStorage(_config["FirebaseStorage:Bucket"]);
                    var imageUrl = await firebaseStorage
                        .Child(_config[$"FirebaseStorage:Folders:{requestDto.Folder.ToString()}"])
                        .Child(fileName)
                        .PutAsync(stream);
            
                    response.Add(imageUrl);
                }
                catch (Exception ex)
                {
                    throw new Exception( $"An error occurred: {ex.Message}");
                }
            }

            return response;
        }
    }
}