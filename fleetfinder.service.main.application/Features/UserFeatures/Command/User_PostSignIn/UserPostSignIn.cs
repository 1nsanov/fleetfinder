using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_PostSignIn;

public static partial class UserPostSignIn
{
    public record Query(UserPostSignIn.RequestDto RequestDto) : IQueryRequest<UserPostSignIn.ResponseDto>;
    
    internal class Handler : IRequestHandler<Query, UserPostSignIn.ResponseDto>
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

        public async Task<UserPostSignIn.ResponseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _userService.GetUserByLoginPassword(request.RequestDto.Login, request.RequestDto.Password, cancellationToken);

            var token = _identifyService.GenerateTokenUser(entity);

            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new UserPostSignIn.ResponseDto(token);
        }
    }
}