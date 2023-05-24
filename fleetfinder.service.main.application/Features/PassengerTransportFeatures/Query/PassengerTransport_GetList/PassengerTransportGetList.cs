using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.domain.Transport.Passenger;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_GetList;

public static partial class PassengerTransportGetList
{
    public record Query(
        int PageSize,
        int SkipCount,
        TransportSortParameter SortParameter,
        bool SortDesc,
        RequestFilter? RequestFilter) : IQueryRequest<ResponseDto>;
    
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
            var query = _queryDbContext.PassengerTransport
                .Include(ct => ct.User)
                .AsQueryable();

            query = Sort(query, request.SortParameter);
            if (request.SortDesc)
                query = query.Reverse();
            query = Filter(query, request.RequestFilter);
            
            var totalCount = query.Count();
            if (totalCount == 0)
                return new ResponseDto(new List<PassengerTransportDto>(), 0);
            
            var entity = await query
                .Skip(request.SkipCount)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            
            return new ResponseDto(_mapper.Map<List<PassengerTransport>, List<PassengerTransportDto>>(entity), totalCount);
        }

        private IQueryable<PassengerTransport> Sort(IQueryable<PassengerTransport> query, TransportSortParameter sortParameter)
        {
            return sortParameter switch
            {
                TransportSortParameter.Default => query.OrderBy(ct => ct.Id),
                TransportSortParameter.PricePerHour => query.OrderBy(ct => ct.Price.PerHour),
                TransportSortParameter.PricePerShift => query.OrderBy(ct => ct.Price.PerShift),
                TransportSortParameter.PricePerKm => query.OrderBy(ct => ct.Price.PerKm),
                _ => query.OrderBy(ct => ct.Id)
            };
        }

        private IQueryable<PassengerTransport> Filter(IQueryable<PassengerTransport> query, RequestFilter? requestFilter)
        {
            if (requestFilter is null)
                return query;

            if (requestFilter.UserFilter is not null)
                query = query.Where(ct => ct.UserId == requestFilter.UserFilter);
            
            if (requestFilter.TitleFilter is not null)
                query = query.Where(ct => EF.Functions.ILike(ct.Title, $"%{requestFilter.TitleFilter}%"));

            if (requestFilter.RegionFilter is not null)
                query = query.Where(ct => ct.Region == requestFilter.RegionFilter);
            
            if (requestFilter.TypeFilter is not null)
                query = query.Where(ct => ct.Type == requestFilter.TypeFilter);

            return query;
        }
    }
}