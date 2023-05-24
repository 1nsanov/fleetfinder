using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Special;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Command.UserProfile_Post;

public static partial class UserProfilePost
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
                RuleFor(x => x.Title)
                    .NotEmpty().WithName("Заголовок").WithMessage("Поле '{PropertyName}' не может быть пустым.")
                    .MaximumLength(100).WithName("Заголовок")
                    .WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.");

                RuleFor(x => x.Brand)
                    .MaximumLength(50).WithName("Бренд")
                    .WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.")
                    .Unless(x => string.IsNullOrEmpty(x.Brand));

                RuleFor(x => x.Price)
                    .SetValidator(new PriceDtoValidator());

                RuleFor(x => x.Description)
                    .MaximumLength(1000).WithName("Описание")
                    .WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.")
                    .Unless(x => string.IsNullOrEmpty(x.Description));

                RuleFor(x => x.Images)
                    .SetValidator(new ImagesValidator());
            }
        }

        public class PriceDtoValidator : AbstractValidator<PriceDto>
        {
            public PriceDtoValidator()
            {
                RuleFor(x => x.PerHour)
                    .GreaterThan(0).WithName("Цена за час").WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);

                RuleFor(x => x.PerShift)
                    .GreaterThan(0).WithName("Цена за смену")
                    .WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);

                RuleFor(x => x.PerKm)
                    .GreaterThan(0).WithName("Цена за километр")
                    .WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);
            }
        }
        
        public class ImagesValidator : AbstractValidator<List<string>>
        {
            public ImagesValidator()
            {
                RuleFor(x => x)
                    .ForEach(image => 
                    {
                        image.NotEmpty().WithMessage("Ссылка на изображение не может быть пустой.")
                            .Matches(@"^https?://[^\s/$.?#].[^\s]*$").WithMessage("Ссылка на изображение должна быть корректной URL-адресом.");
                    })
                    .Unless(x => x.Count == 0);
            }
        }
    }

    #endregion

    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, SpecialTransport>
    {
        public partial SpecialTransport Map(RequestDto source);

        private List<SpecialTransportImage> Map(List<string> source)
            => source.ConvertAll(img => new SpecialTransportImage { Url = img });
    }
}