using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly ICommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        private readonly IIdentifyService _identifyService;
        private readonly IPasswordService _passwordService;

        public Handler(
            ICommandDbContext commandDbContext,
            IMapper mapper,
            IIdentifyService identifyService,
            IPasswordService passwordService)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
            _identifyService = identifyService;
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
            
            var token = _identifyService.GenerateTokenUser(entity);
            
            return new ResponseDto(token);
        }
    }
}
