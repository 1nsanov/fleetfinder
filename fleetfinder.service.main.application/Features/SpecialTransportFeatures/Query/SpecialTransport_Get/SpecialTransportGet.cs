using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.domain.Transport.Special;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_Get;

public static partial class SpecialTransportGet
{
    public record Query(long Id) : IQueryRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Query, ResponseDto>
    {
        private readonly QueryDbContext _queryDbContext;
        private readonly IMapper _mapper;

        public Handler(QueryDbContext queryDbContext, IMapper mapper)
        {
            _queryDbContext = queryDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _queryDbContext.SpecialTransport
                             .Include(ct => ct.User)
                             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                                ?? throw new EntityNotFoundException(request.Id);
            
            return _mapper.Map<SpecialTransport, ResponseDto>(entity);
        }
    }
}