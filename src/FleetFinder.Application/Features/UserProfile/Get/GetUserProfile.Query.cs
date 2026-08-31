using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Features.UserProfile.Get;

public static partial class GetUserProfile
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
            var entity = await _queryDbContext.User
                             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                                ?? throw new EntityNotFoundException(request.Id);
            
            return _mapper.Map<User, ResponseDto>(entity);
        }
    }
}
