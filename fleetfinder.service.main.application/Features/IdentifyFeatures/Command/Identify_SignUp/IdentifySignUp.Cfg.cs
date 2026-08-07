using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

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

    #endregion

    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, User>
    {
        [MapperIgnoreTarget(nameof(User.ImageUrl))]
        [MapperIgnoreTarget(nameof(User.Contact))]
        [MapperIgnoreTarget(nameof(User.RefreshToken))]
        [MapperIgnoreTarget(nameof(User.CargoTransports))]
        [MapperIgnoreTarget(nameof(User.PassengerTransports))]
        [MapperIgnoreTarget(nameof(User.SpecialTransports))]
        [MapperIgnoreTarget(nameof(User.CargoOrders))]
        [MapperIgnoreTarget(nameof(User.PassengerOrders))]
        [MapperIgnoreTarget(nameof(User.SpecialOrders))]
        [MapperIgnoreTarget(nameof(User.Id))]
        [MapperIgnoreTarget(nameof(User.CreateDate))]
        [MapperIgnoreTarget(nameof(User.UpdateDate))]
        [MapperIgnoreTarget(nameof(User.State))]
        public partial User Map(RequestDto source);
    }
}