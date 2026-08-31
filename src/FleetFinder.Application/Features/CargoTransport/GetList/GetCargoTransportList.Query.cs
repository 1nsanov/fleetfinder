using FleetFinder.Application.Common.Enums;
using FleetFinder.Domain.Transport.Cargo;
using Microsoft.EntityFrameworkCore;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;

namespace FleetFinder.Application.Features.CargoTransport.GetList;

public static partial class GetCargoTransportList
{
    public record Query(
        int PageSize,
        int SkipCount,
        TransportSortParameter SortParameter,
        bool SortDesc,
        RequestFilter? RequestFilter) : IQueryRequest<ResponseDto>;
    
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
            var query = _queryDbContext.CargoTransport
                .Include(ct => ct.User)
                .AsQueryable();

            query = Sort(query, request.SortParameter);
            if (request.SortDesc)
                query = query.Reverse();
            query = Filter(query, request.RequestFilter);
            
            var totalCount = query.Count();
            if (totalCount == 0)
                return new ResponseDto(new List<CargoTransportDto>(), 0);
            
            var entity = await query
                .Skip(request.SkipCount)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            
            return new ResponseDto(_mapper.Map<List<CargoEntity>, List<CargoTransportDto>>(entity), totalCount);
        }

        private IQueryable<CargoEntity> Sort(IQueryable<CargoEntity> query, TransportSortParameter sortParameter)
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

        private IQueryable<CargoEntity> Filter(IQueryable<CargoEntity> query, RequestFilter? requestFilter)
        {
            if (requestFilter is null)
                return query;

            if (requestFilter.UserFilter is not null)
                query = query.Where(ct => ct.UserId == requestFilter.UserFilter);
            
            if (requestFilter.TitleFilter is not null)
                query = query.Where(ct => ct.Title.ToLower().Contains(requestFilter.TitleFilter.ToLower()));

            if (requestFilter.RegionFilter is not null)
                query = query.Where(ct => ct.Region == requestFilter.RegionFilter);
            
            if (requestFilter.TypeFilter is not null)
                query = query.Where(ct => ct.Type == requestFilter.TypeFilter);

            return query;
        }
    }
}
