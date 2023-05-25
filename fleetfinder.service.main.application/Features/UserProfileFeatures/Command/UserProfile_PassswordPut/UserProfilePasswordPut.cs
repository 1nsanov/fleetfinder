using fleetfinder.service.main.application.Common.Exceptions;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_PassswordPut;

public static partial class UserProfilePasswordPut
{
    public record Command(long UserId, RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    internal class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        
        public Handler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _commandDbContext.User.FindAsync(new object[] { request.UserId }, cancellationToken)
                         ?? throw new EntityNotFoundException(request.UserId);

            if (entity.Password != request.RequestDto.CurrentPassword)
                throw new ValidationException("Неверный текущий пароль.");
            
            if (request.RequestDto.CurrentPassword == request.RequestDto.NewPassword)
                throw new ValidationException("Текущий и новый пароль совпадают.");
            
            entity.Password = request.RequestDto.NewPassword;
            
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto(true);
        }
    }
}