using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Features.Identity.SignUp;

public static partial class SignUp
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly ICommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IPasswordService _passwordService;

        public Handler(
            ICommandDbContext commandDbContext,
            IMapper mapper,
            IIdentityService identityService,
            IPasswordService passwordService)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
            _identityService = identityService;
            _passwordService = passwordService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            var duplicateLogin = await _commandDbContext.User.FirstOrDefaultAsync(u => u.Login == requestDto.Login, cancellationToken: cancellationToken);
            if (duplicateLogin is not null)
                throw new ValidationException("A user with that username already exists.");

            var entity = _mapper.Map<RequestDto, User>(requestDto);
            entity.Password = _passwordService.HashPassword(requestDto.Password);
            
            await _commandDbContext.User.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            var token = _identityService.GenerateTokenUser(entity);
            
            return new ResponseDto(token);
        }
    }
}
