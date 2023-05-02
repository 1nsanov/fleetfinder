namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.User_Get;

public static partial class UserGet
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
            var entity = _queryDbContext.User.FirstOrDefault(u => u.Id == request.Id);

            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return _mapper.Map<ResponseDto>(entity);
        }
    }
}