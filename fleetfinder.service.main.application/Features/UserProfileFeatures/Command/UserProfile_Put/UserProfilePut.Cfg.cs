using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_Put;

public static partial class UserProfilePut
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
                RuleFor(dto => dto.Email).NotEmpty().EmailAddress()
                    .WithName("Эл. почта").WithMessage("Поле {PropertyName} имеет не верный формат.");
                RuleFor(dto => dto.FullName).SetValidator(new NameValidator());
                RuleFor(dto => dto.ImageUrl).NotEmpty().WithMessage("Ссылка на изображение не может быть пустой.")
                    .Matches(@"^https?://[^\s/$.?#].[^\s]*$").WithMessage("8Ссылка на изображение должна быть корректной URL-адресом.")
                    .Unless(dto => dto.ImageUrl is null);
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

    #endregion

    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, User>
    {
        public partial User Map(RequestDto source);
    }
}