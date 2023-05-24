using fleetfinder.service.main.domain.Transport.Passenger;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Command.PassengerTransport_Post;

public static partial class PassengerTransportPost
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
            var entity = _mapper.Map<RequestDto, PassengerTransport>(request.RequestDto);
            entity.UserId = request.UserId;
            
            await _commandDbContext.PassengerTransport.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}