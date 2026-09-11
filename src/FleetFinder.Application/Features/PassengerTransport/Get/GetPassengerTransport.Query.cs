using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Transport.Passenger;
using Microsoft.EntityFrameworkCore;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;

namespace FleetFinder.Application.Features.PassengerTransport.Get;

public static partial class GetPassengerTransport
{
    public record Query(long Id) : IQueryRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Query, ResponseDto>
    {
        private readonly IQueryDbContext _queryDbContext;
        private readonly IMapper _mapper;

        public Handler(IQueryDbContext queryDbContext, IMapper mapper)
        {
            _queryDbContext = queryDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _queryDbContext.PassengerTransport
                             .Include(ct => ct.User)
                             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                                ?? throw new EntityNotFoundException(request.Id);
            
            return _mapper.Map<PassengerEntity, ResponseDto>(entity);
        }
    }
}
