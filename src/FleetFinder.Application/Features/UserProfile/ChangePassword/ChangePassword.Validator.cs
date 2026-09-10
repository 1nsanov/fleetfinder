namespace FleetFinder.Application.Features.UserProfile.ChangePassword;

public static partial class ChangePassword
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
                RuleFor(dto => dto.NewPassword).NotEmpty()
                    .MinimumLength(8).WithName("New password").WithMessage(ValidationMessages.MinLength)
                    .MaximumLength(100).WithMessage(ValidationMessages.MaxLength);
            }
        }
    }
}
