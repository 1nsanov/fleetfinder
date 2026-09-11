using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Transport.Passenger;
using Microsoft.EntityFrameworkCore;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;
using PassengerImageEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransportImage;


namespace FleetFinder.Application.Features.PassengerTransport.Update;

public static partial class UpdatePassengerTransport
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
            var entity = await _commandDbContext.PassengerTransport
                             .FirstOrDefaultAsync(ct => ct.Id == request.RequestDto.Id && ct.UserId == request.UserId, cancellationToken) 
                         ?? throw new EntityNotFoundException(request.RequestDto.Id);
            
            var updated = _mapper.Map<RequestDto, PassengerEntity>(request.RequestDto);

            var updatedImage = new List<PassengerImageEntity>();
            foreach (var image in updated.Images)
            {
                var exist = entity.Images.FirstOrDefault(cti => cti.Url == image.Url);
                updatedImage.Add(exist ?? image);
            }

            entity.Price = updated.Price;
            entity.Size = updated.Size;
            entity.Images = updatedImage;
            _commandDbContext.Entry(entity).CurrentValues.SetValues(updated);
            _commandDbContext.Entry(entity).Property(ct => ct.UserId).IsModified = false;
            _commandDbContext.Entry(entity).Property(ct => ct.CreateDate).IsModified = false;
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(entity.Id);
        }
    }
}
