using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Transport.Special;
using Microsoft.EntityFrameworkCore;
using SpecialEntity = FleetFinder.Domain.Transport.Special.SpecialTransport;

namespace FleetFinder.Application.Features.SpecialTransport.Get;

public static partial class GetSpecialTransport
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
            var entity = await _queryDbContext.SpecialTransport
                             .Include(ct => ct.User)
                             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                                ?? throw new EntityNotFoundException(request.Id);
            
            return _mapper.Map<SpecialEntity, ResponseDto>(entity);
        }
    }
}
