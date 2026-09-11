using FleetFinder.Application.Common.FeatureModels;

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
                    .MinimumLength(4).WithName("Login").WithMessage(ValidationMessages.MinLength)
                    .MaximumLength(16).WithName("Login").WithMessage(ValidationMessages.MaxLength);
                RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8).MaximumLength(100)
                    .WithName("Password").WithMessage(ValidationMessages.MinLength);
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress()
                    .WithName("Email").WithMessage(ValidationMessages.InvalidEmail);
                RuleFor(dto => dto.FullName).SetValidator(new NameValidator());
            }
        }

        internal class NameValidator : AbstractValidator<FullNameDto>
        {
            public NameValidator()
            {
                RuleFor(name => name.First).NotEmpty().MaximumLength(50)
                    .WithName("First name").WithMessage(ValidationMessages.MaxLength);
                RuleFor(name => name.Second).NotEmpty().MaximumLength(50)
                    .WithName("Last name").WithMessage(ValidationMessages.MaxLength);
                RuleFor(name => name.Surname).NotEmpty().MaximumLength(50)
                    .WithName("Middle name").WithMessage(ValidationMessages.MaxLength)
                    .Unless(name => string.IsNullOrWhiteSpace(name.Surname));
            }
        }
    }
}
