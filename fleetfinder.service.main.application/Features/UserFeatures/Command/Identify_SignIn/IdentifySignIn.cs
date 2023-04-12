using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_SignIn;

public static partial class IdentifySignIn
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
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
            var entity = await _userService.GetUserByLoginPassword(request.RequestDto.Login, request.RequestDto.Password, cancellationToken);

            var token = _identifyService.GenerateTokenUser(entity);

            _commandDbContext.Users.Update(entity);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(token);
        }
    }
}