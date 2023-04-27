using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_RefreshToken;

public static partial class IdentifyRefreshToken
{
    public record Command(string? AccessToken, string? RefreshToken) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentifyService _identifyService;
        private readonly CommandDbContext _commandDbContext;

        public Handler(IIdentifyService identifyService, CommandDbContext commandDbContext)
        {
            _identifyService = identifyService;
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null || request.RefreshToken is null)
                return new ResponseDto(null);
            
            var entity = await _identifyService.GetUserByAccessToken(request.AccessToken, cancellationToken);

            var validToken = entity.RefreshToken?.Value == request.RefreshToken &&
                             entity.RefreshToken.ExpiryTime < DateTime.UtcNow;
            if (!validToken) throw new SecurityTokenValidationException("Invalid access token or refresh token");

            var token = _identifyService.GenerateTokenUser(entity);
            
            _commandDbContext.Users.Update(entity);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(token);
        }
    }
}