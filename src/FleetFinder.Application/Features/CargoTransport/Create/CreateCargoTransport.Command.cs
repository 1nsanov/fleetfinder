using FleetFinder.Domain.Transport.Cargo;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;

namespace FleetFinder.Application.Features.CargoTransport.Create;

public static partial class CreateCargoTransport
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
            var entity = _mapper.Map<RequestDto, CargoEntity>(request.RequestDto);
            entity.UserId = request.UserId;
            
            await _commandDbContext.CargoTransport.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}
