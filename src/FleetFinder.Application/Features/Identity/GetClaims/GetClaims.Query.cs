using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Features.Identity.GetClaims;

public static partial class GetClaims
{
    public record Query(string? AccessToken) : IQueryRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Query, ResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IQueryDbContext _queryDbContext;

        public Handler(IIdentityService identityService, IQueryDbContext queryDbContext)
        {
            _identityService = identityService;
            _queryDbContext = queryDbContext;
        }

        public async Task<ResponseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) 
                return new ResponseDto(null, null, null);
            
            var principal = _identityService.GetPrincipalFromToken(request.AccessToken, true);
            var claimSid = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("sid"))?.Value;
            if (string.IsNullOrEmpty(claimSid))
            {
                throw new ArgumentNullException(nameof(claimSid));
            }
            
            var userId = long.Parse(claimSid);
            var user = await _queryDbContext.User.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new EntityNotFoundException(userId);

            var fullName = $"{user.FullName.First} {user.FullName.Second} {user.FullName.Surname}";
            
            return new ResponseDto(userId, fullName, user.ImageUrl);
        }
    }
}
