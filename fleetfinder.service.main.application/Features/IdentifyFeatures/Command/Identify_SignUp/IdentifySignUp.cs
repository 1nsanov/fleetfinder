using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    public record Command(RequestDto RequestDto) : ICommandRequest<ResponseDto>;
    
    public class Handler : IRequestHandler<Command, ResponseDto>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly IMapper _mapper;
        private readonly IIdentifyService _identifyService;

        public Handler(CommandDbContext commandDbContext, IMapper mapper, IIdentifyService identifyService)
        {
            _commandDbContext = commandDbContext;
            _mapper = mapper;
            _identifyService = identifyService;
        }

        public async Task<ResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;

            var duplicateLogin = await _commandDbContext.User.FirstOrDefaultAsync(u => u.Login == requestDto.Login, cancellationToken: cancellationToken);
            if (duplicateLogin is not null)
                throw new ValidationException("Пользователь с таким логином уже существует.");

            var entity = _mapper.Map<RequestDto, User>(requestDto); 
            
            await _commandDbContext.User.AddAsync(entity, cancellationToken);
            await _commandDbContext.SaveChangesAsync(cancellationToken);
            
            var token = _identifyService.GenerateTokenUser(entity);
            
            return new ResponseDto(token);
        }
    }
}