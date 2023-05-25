namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_PassswordPut;

public static partial class UserProfilePasswordPut
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
                RuleFor(dto => dto.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(100)
                    .WithName("Пароль").WithMessage("Поле '{PropertyName}' не может быть меньше {MinLength} символов.");
            }
        }
    }

    #endregion
}