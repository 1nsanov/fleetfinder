﻿using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_GetClaims;

public static partial class IdentifyGetClaims
{
    public record Command(string? AccessToken) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentifyService _identifyService;
        private readonly IUserService _userService;
        private readonly CommandDbContext _commandDbContext; 

        public Handler(IIdentifyService identifyService, IUserService userService, CommandDbContext commandDbContext)
        {
            _identifyService = identifyService;
            _userService = userService;
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.AccessToken is null) throw new ArgumentNullException(nameof(request.AccessToken));
            
            var principal = _identifyService.GetPrincipalFromToken(request.AccessToken, true);
            var claimSid = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("sid"))?.Value;
            var userId = long.Parse(claimSid);
            var claimGivenName = principal.Claims.FirstOrDefault(claim => claim.Type.Contains("givenname"))?.Value;

            return new ResponseDto(userId, claimGivenName);
        }
    }
}