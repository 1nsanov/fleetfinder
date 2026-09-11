using FleetFinder.Application.Common.FeatureModels;

namespace FleetFinder.Application.Features.UserProfile.Update;

public static partial class UpdateUserProfile
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
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress()
                    .WithName("Email").WithMessage(ValidationMessages.InvalidEmail);
                RuleFor(dto => dto.FullName).SetValidator(new NameValidator());
                RuleFor(dto => dto.ImageUrl).NotEmpty().WithMessage(ValidationMessages.ImageUrlRequired)
                    .Matches(@"^https?://[^\s/$.?#].[^\s]*$").WithMessage(ValidationMessages.ImageUrlInvalid)
                    .Unless(dto => dto.ImageUrl is null);
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
