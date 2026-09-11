using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using Microsoft.IdentityModel.Tokens;

namespace FleetFinder.Application.Features.Identity.Refresh;

public static partial class Refresh
{
    public record Command(string? AccessToken, string? RefreshToken) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICommandDbContext _commandDbContext;

        public Handler(IIdentityService identityService, ICommandDbContext commandDbContext)
        {
            _identityService = identityService;
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null || request.RefreshToken is null)
                return new ResponseDto(null);
            
            var entity = await _identityService.GetUserByAccessToken(request.AccessToken, cancellationToken);

            var validToken = entity.RefreshToken?.Value == request.RefreshToken &&
                             entity.RefreshToken.ExpiryTime > DateTime.UtcNow;
            if (!validToken) throw new SecurityTokenValidationException("Invalid access token or refresh token");

            var token = _identityService.GenerateTokenUser(entity);
            
            _commandDbContext.User.Update(entity);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(token);
        }
    }
}
