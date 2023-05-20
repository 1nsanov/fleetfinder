using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.domain.Transport.Cargo;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Put;

public static partial class CargoTransportPut
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
            var entity = await _commandDbContext.CargoTransport
                             .FirstOrDefaultAsync(ct => ct.Id == request.RequestDto.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.RequestDto.Id);
            
            var updated = _mapper.Map<RequestDto, CargoTransport>(request.RequestDto);
            entity.Price = updated.Price;
            entity.Body = updated.Body;
            entity.Images = updated.Images;
            _commandDbContext.Entry(entity).CurrentValues.SetValues(updated);
            _commandDbContext.Entry(entity).Property(ct => ct.UserId).IsModified = false;
            _commandDbContext.Entry(entity).Property(ct => ct.CreateDate).IsModified = false;
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}