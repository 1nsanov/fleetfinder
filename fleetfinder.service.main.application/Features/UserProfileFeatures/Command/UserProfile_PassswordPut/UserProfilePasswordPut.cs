using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_PassswordPut;

public static partial class UserProfilePasswordPut
{
    public record Command(long UserId, RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly ICommandDbContext _commandDbContext;
        private readonly IPasswordService _passwordService;

        public Handler(ICommandDbContext commandDbContext, IPasswordService passwordService)
        {
            _commandDbContext = commandDbContext;
            _passwordService = passwordService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _commandDbContext.User.FindAsync(new object[] { request.UserId }, cancellationToken)
                         ?? throw new EntityNotFoundException(request.UserId);

            if (!_passwordService.VerifyPassword(request.RequestDto.CurrentPassword, entity.Password))
                throw new ValidationException("Неверный текущий пароль.");
            
            if (request.RequestDto.CurrentPassword == request.RequestDto.NewPassword)
                throw new ValidationException("Текущий и новый пароль совпадают.");
            
            entity.Password = _passwordService.HashPassword(request.RequestDto.NewPassword);
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}
