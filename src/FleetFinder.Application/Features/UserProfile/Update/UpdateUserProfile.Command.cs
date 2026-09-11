using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Domain.Users;

namespace FleetFinder.Application.Features.UserProfile.Update;

public static partial class UpdateUserProfile
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
            var entity = await _commandDbContext.User.FindAsync(new object[] { request.UserId }, cancellationToken)
                         ?? throw new EntityNotFoundException(request.UserId);
            
            var updated = _mapper.Map<RequestDto, User>(request.RequestDto);
            updated.Id = request.UserId;
            entity.Contact = updated.Contact;
            entity.FullName = updated.FullName;
            
            _commandDbContext.Entry(entity).CurrentValues.SetValues(updated);
            _commandDbContext.Entry(entity).Property(u => u.CreateDate).IsModified = false;
            _commandDbContext.Entry(entity).Property(u => u.Login).IsModified = false;
            _commandDbContext.Entry(entity).Property(u => u.Password).IsModified = false;
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}
