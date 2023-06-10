using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.Identify_GetClaims;

public static partial class IdentifyGetClaims
{
    public record Command(string? AccessToken) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentifyService _identifyService;
        private readonly QueryDbContext _queryDbContext;

        public Handler(IIdentifyService identifyService, QueryDbContext queryDbContext)
        {
            _identifyService = identifyService;
            _queryDbContext = queryDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) 
                return new ResponseDto(null, null, null);
            
            var principal = _identifyService.GetPrincipalFromToken(request.AccessToken, true);
            var claimSid = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("sid"))?.Value;
            var userId = long.Parse(claimSid);
            var givenName = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("givenname"))?.Value;

            var user = await _queryDbContext.User.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new EntityNotFoundException(userId);
            
            return new ResponseDto(userId, givenName, user.ImageUrl);
        }
    }
}