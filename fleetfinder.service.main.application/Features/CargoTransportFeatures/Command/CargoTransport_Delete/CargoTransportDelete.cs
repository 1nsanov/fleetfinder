using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.domain.Enums.Common;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Delete;

public static partial class CargoTransportDelete
{
    public record Command(long UserId, long Id) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        
        public Handler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _commandDbContext.CargoTransport
                             .FirstOrDefaultAsync(ct => ct.Id == request.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.Id);

            entity.State = State.Archived;
            entity.Images.ForEach(image => image.State = State.Archived);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}