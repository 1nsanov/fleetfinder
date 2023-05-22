using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.domain.Transport.Special;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Command.SpecialTransport_Put;

public static partial class SpecialTransportPut
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
            var entity = await _commandDbContext.SpecialTransport
                             .FirstOrDefaultAsync(ct => ct.Id == request.RequestDto.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.RequestDto.Id);
            
            var updated = _mapper.Map<RequestDto, SpecialTransport>(request.RequestDto);

            var updatedImage = new List<SpecialTransportImage>();
            foreach (var image in updated.Images)
            {
                var exist = entity.Images.FirstOrDefault(cti => cti.Url == image.Url);
                updatedImage.Add(exist ?? image);
            }

            entity.Price = updated.Price;
            entity.Images = updatedImage;
            _commandDbContext.Entry(entity).CurrentValues.SetValues(updated);
            _commandDbContext.Entry(entity).Property(ct => ct.UserId).IsModified = false;
            _commandDbContext.Entry(entity).Property(ct => ct.CreateDate).IsModified = false;
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}