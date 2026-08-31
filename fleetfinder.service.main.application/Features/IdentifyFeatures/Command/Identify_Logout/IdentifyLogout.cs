using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_Logout;

public static partial class IdentifyLogout
{
    public record Command(string? AccessToken) : ICommandRequest<bool>;
    
    internal class Handler : IRequestHandler<Command, bool>
    {
        private readonly IIdentifyService _identifyService;
        private readonly ICommandDbContext _commandDbContext;

        public Handler(IIdentifyService identifyService, ICommandDbContext commandDbContext)
        {
            _identifyService = identifyService;
            _commandDbContext = commandDbContext;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) throw new ArgumentNullException(nameof(request.AccessToken));
            
            var entity = await _identifyService.GetUserByAccessToken(request.AccessToken, cancellationToken)
                    ?? throw new ArgumentNullException("Invalid Access Token!");

            entity.RefreshToken = null;
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
