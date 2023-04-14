using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_SignUp;

public static partial class IdentifySignUp
{
    #region Validator

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(cmd => cmd.RequestDto).SetValidator(new RequestValidator());
        }
        
        internal class RequestValidator : AbstractValidator<RequestDto>
        {
            public RequestValidator()
            {
                RuleFor(dto => dto.Login).NotEmpty().MinimumLength(4).MaximumLength(16);
                RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
                RuleFor(dto => dto.Name).SetValidator(new NameValidator());
            }
        }
        
        internal class NameValidator : AbstractValidator<Name>
        {
            public NameValidator()
            {
                RuleFor(name => name.Last).NotEmpty().MaximumLength(50);
                RuleFor(name => name.First).NotEmpty().MaximumLength(50);
                RuleFor(name => name.Middle).NotEmpty().MaximumLength(50);
            }
        }
    }

    #endregion

    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RequestDto, User>()
                .MapRecordMember(dest => dest.FullName, src => $"{src.Name.First} {src.Name.Last} {src.Name.Middle}");
        }
    }

    #endregion
}