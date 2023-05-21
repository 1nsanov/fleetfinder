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
        
        public Handler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            foreach (var url in requestDto.Urls)
            {
                try
                {
                    var firebaseStorage = new FirebaseStorage(_config["FirebaseStorage:Bucket"]);

                    var fileName = url
                        .Split("/").Last()
                        .Split("?").First();
            
                    await firebaseStorage
                        .Child(_config[$"FirebaseStorage:Folders:{requestDto.Folder.ToString()}"])
                        .Child(fileName)
                        .DeleteAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception( $"An error occurred: {ex.Message}");
                }
            }
            
            return true;
        }
    }
}