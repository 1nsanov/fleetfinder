using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_Post;

public static partial class UserPost
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly IMapper _mapper;

        public Handler(CommandDbContext commandDbContext, IMapper mapper)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var req = request.RequestDto;

            var entity = _mapper.Map<User>(req);

            await _commandDbContext.Users.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return new ResponseDto(entity.Id);
        }
    }
}