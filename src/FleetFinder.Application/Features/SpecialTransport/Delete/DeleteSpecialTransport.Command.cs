using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Enums.Common;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Features.SpecialTransport.Delete;

public static partial class DeleteSpecialTransport
{
    public record Command(long UserId, long Id) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly ICommandDbContext _commandDbContext;
        
        public Handler(ICommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _commandDbContext.SpecialTransport
                             .FirstOrDefaultAsync(ct => ct.Id == request.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.Id);

            entity.State = State.Archived;
            entity.Images.ForEach(image => image.State = State.Archived);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}
