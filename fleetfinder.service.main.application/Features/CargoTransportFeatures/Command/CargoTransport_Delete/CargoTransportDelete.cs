﻿using fleetfinder.service.main.application.Common.Exceptions;
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
                             .Include(ct => ct.Images)
                             .FirstOrDefaultAsync(ct => ct.Id == request.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.Id);

            _commandDbContext.CargoTransport.Remove(entity);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}