using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;

namespace FleetFinder.Application.Features.Identity.SignIn;

public static partial class SignIn
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly ICommandDbContext _commandDbContext; 

        public Handler(IIdentityService identityService, IUserService userService, ICommandDbContext commandDbContext)
        {
            _identityService = identityService;
            _userService = userService;
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _userService.GetUserByLoginPassword(request.RequestDto.Login, request.RequestDto.Password, cancellationToken);

            var token = _identityService.GenerateTokenUser(entity);

            _commandDbContext.User.Update(entity);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(token);
        }
    }
}
