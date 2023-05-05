using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.Identify_GetClaims;

public static partial class IdentifyGetClaims
{
    public record Command(string? AccessToken) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentifyService _identifyService;

        public Handler(IIdentifyService identifyService)
        {
            _identifyService = identifyService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) 
                return new ResponseDto(null, null);
            
            var principal = _identifyService.GetPrincipalFromToken(request.AccessToken, true);
            var claimSid = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("sid"))?.Value;
            var userId = long.Parse(claimSid);
            var claimGivenName = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("givenname"))?.Value;
            
            return new ResponseDto(userId, claimGivenName);
        }
    }
}