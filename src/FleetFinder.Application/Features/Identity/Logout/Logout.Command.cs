using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;

namespace FleetFinder.Application.Features.Identity.Logout;

public static partial class Logout
{
    public record Command(string? AccessToken) : ICommandRequest<bool>;
    
    internal class Handler : IRequestHandler<Command, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ICommandDbContext _commandDbContext;

        public Handler(IIdentityService identityService, ICommandDbContext commandDbContext)
        {
            _identityService = identityService;
            _commandDbContext = commandDbContext;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) throw new ArgumentNullException(nameof(request.AccessToken));
            
            var entity = await _identityService.GetUserByAccessToken(request.AccessToken, cancellationToken)
                    ?? throw new ArgumentNullException("Invalid Access Token!");

            entity.RefreshToken = null;
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
