using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        private readonly IIdentifyService _identifyService;

        public Handler(CommandDbContext commandDbContext, IMapper mapper, IIdentifyService identifyService)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
            _identifyService = identifyService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            var entity = _mapper.Map<User>(requestDto); 

            var token = _identifyService.GenerateTokenUser(entity);
            
            await _commandDbContext.Users.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            return new ResponseDto(token);
        }
    }
}