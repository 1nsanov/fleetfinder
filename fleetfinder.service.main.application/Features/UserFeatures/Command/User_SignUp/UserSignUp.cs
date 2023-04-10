using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_SignUp;

public static partial class UserSignUp
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public Handler(CommandDbContext commandDbContext, IMapper mapper, IUserService userService)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            var entity = _mapper.Map<User>(requestDto); 

            var token = _userService.GenerateTokenUser(entity);
            entity.RefreshToken.Value = token.Refresh;
            entity.RefreshToken.ExpiryTime = token.Expiration;

            await _commandDbContext.Users.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return new ResponseDto(token);
        }
    }
}