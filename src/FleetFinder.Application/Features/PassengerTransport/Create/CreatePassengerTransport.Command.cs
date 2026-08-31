using FleetFinder.Domain.Transport.Passenger;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;

namespace FleetFinder.Application.Features.PassengerTransport.Create;

public static partial class CreatePassengerTransport
{
    public record Command(long UserId, RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly ICommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        
        public Handler(ICommandDbContext commandDbContext, IMapper mapper)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RequestDto, PassengerEntity>(request.RequestDto);
            entity.UserId = request.UserId;
            
            await _commandDbContext.PassengerTransport.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}
