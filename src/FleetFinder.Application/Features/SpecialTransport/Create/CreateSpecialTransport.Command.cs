using FleetFinder.Domain.Transport.Special;
using SpecialEntity = FleetFinder.Domain.Transport.Special.SpecialTransport;

namespace FleetFinder.Application.Features.SpecialTransport.Create;

public static partial class CreateSpecialTransport
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
            var entity = _mapper.Map<RequestDto, SpecialEntity>(request.RequestDto);
            entity.UserId = request.UserId;
            
            await _commandDbContext.SpecialTransport.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}
