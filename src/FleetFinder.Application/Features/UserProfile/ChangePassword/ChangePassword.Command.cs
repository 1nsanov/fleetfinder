using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;

namespace FleetFinder.Application.Features.UserProfile.ChangePassword;

public static partial class ChangePassword
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
