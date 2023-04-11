using System.Security.Authentication;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Services;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_Logout;

public static partial class UserLogout
{
    public record Command(string? AccessToken) : ICommandRequest<bool>;
    
    internal class Handler : IRequestHandler<Command, bool>
    {
        private readonly IIdentifyService _identifyService;
        private readonly CommandDbContext _commandDbContext;
        private readonly TokenService _tokenService;

        public Handler(IIdentifyService identifyService, CommandDbContext commandDbContext, TokenService tokenService)
        {
            _identifyService = identifyService;
            _commandDbContext = commandDbContext;
            _tokenService = tokenService;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) throw new ArgumentNullException(nameof(request.AccessToken));
            
            var entity = await _identifyService.GetUserByAccessToken(request.AccessToken, cancellationToken)
                    ?? throw new AuthenticationException("Invalid Access Token!");

            _tokenService.RevokeToken(request.AccessToken);
            
            entity.RefreshToken = null;
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}