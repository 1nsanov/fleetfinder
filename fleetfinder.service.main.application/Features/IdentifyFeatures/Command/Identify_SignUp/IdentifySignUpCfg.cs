using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Command.Identify_SignUp;

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
                RuleFor(dto => dto.Login).NotEmpty().MinimumLength(4).MaximumLength(16).WithName("Логин");
                RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8).MaximumLength(100).WithName("Пароль");
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithName("Эл. почта");
                RuleFor(dto => dto.FullName).SetValidator(new NameValidator());
            }
        }
        
        internal class NameValidator : AbstractValidator<FullName>
        {
            public NameValidator()
            {
                RuleFor(name => name.First).NotEmpty().MaximumLength(50).WithName("Имя");
                RuleFor(name => name.Second).NotEmpty().MaximumLength(50).WithName("Фамилия");;
                RuleFor(name => name.Surname).NotEmpty().MaximumLength(50).Unless(name => name.Surname is null).WithName("Отчество");
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
                .MapRecordMember(dest => dest.FullName,
                    src => new domain.Users.FullName
                    {
                        First = src.FullName.First,
                        Second = src.FullName.Second,
                        Surname = src.FullName.Surname
                    });
        }
    }

    #endregion
}