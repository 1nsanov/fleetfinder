using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Users;


namespace FleetFinder.Application.Features.Identity.SignUp;

public static partial class SignUp
{


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
                RuleFor(dto => dto.Login).NotEmpty()
                    .MinimumLength(4).WithName("Логин").WithMessage("Поле '{PropertyName}' не может быть меньше {MinLength} символов.")
                    .MaximumLength(16).WithName("Логин").WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.");
                RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8).MaximumLength(100)
                    .WithName("Пароль").WithMessage("Поле '{PropertyName}' не может быть меньше {MinLength} символов.");
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress()
                    .WithName("Эл. почта").WithMessage("Поле {PropertyName} имеет не верный формат.");
                RuleFor(dto => dto.FullName).SetValidator(new NameValidator());
            }
        }
        
        internal class NameValidator : AbstractValidator<FullNameDto>
        {
            public NameValidator()
            {
                RuleFor(name => name.First).NotEmpty().MaximumLength(50)
                    .WithName("Имя").WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.");
                RuleFor(name => name.Second).NotEmpty().MaximumLength(50)
                    .WithName("Фамилия").WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.");
                RuleFor(name => name.Surname).NotEmpty().MaximumLength(50)
                    .WithName("Отчество").WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.")
                    .Unless(name => string.IsNullOrWhiteSpace(name.Surname));
            }
        }
    }

    
}
