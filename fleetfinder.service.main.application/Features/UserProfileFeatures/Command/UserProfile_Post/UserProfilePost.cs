using fleetfinder.service.main.domain.Transport.Special;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_Post;

public static partial class UserProfilePost
{
    public record Command(long UserId, RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
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
            var entity = _mapper.Map<RequestDto, SpecialTransport>(request.RequestDto);
            entity.UserId = request.UserId;
            
            await _commandDbContext.SpecialTransport.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}