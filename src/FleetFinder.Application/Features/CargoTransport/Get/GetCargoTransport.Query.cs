using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Transport.Cargo;
using Microsoft.EntityFrameworkCore;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;

namespace FleetFinder.Application.Features.CargoTransport.Get;

public static partial class GetCargoTransport
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
            var entity = await _queryDbContext.CargoTransport
                             .Include(ct => ct.User)
                             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                                ?? throw new EntityNotFoundException(request.Id);
            
            return _mapper.Map<CargoEntity, ResponseDto>(entity);
        }
    }
}
