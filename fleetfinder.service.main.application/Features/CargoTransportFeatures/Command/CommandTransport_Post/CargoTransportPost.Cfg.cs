using fleetfinder.service.main.domain.Bases;
using fleetfinder.service.main.domain.Transport.Cargo;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CommandTransport_Post;

public static partial class CargoTransportPost
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

                RuleFor(x => x.Region)
                    .IsInEnum().WithName("Регион").WithMessage("Поле '{PropertyName}' не может быть пустым.");

                RuleFor(x => x.Brand)
                    .MaximumLength(50).WithName("Бренд")
                    .WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.")
                    .Unless(x => string.IsNullOrEmpty(x.Brand));

                RuleFor(x => x.YearIssue)
                    .InclusiveBetween(new DateOnly(1900, 1, 1), DateOnly.FromDateTime(DateTime.Now))
                    .WithName("Год выпуска").WithMessage("Поле '{PropertyName}' должно быть между {From} и {To}.")
                    .Unless(x => x is null);

                RuleFor(x => x.Price)
                    .SetValidator(new PriceDtoValidator());

                RuleFor(x => x.Description)
                    .MaximumLength(1000).WithName("Описание")
                    .WithMessage("Поле '{PropertyName}' не может превышать {MaxLength} символов.")
                    .Unless(x => string.IsNullOrEmpty(x.Description));

                RuleFor(x => x.Body)
                    .SetValidator(new BodyDtoValidator());

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

        public class BodyDtoValidator : AbstractValidator<BodyDto>
        {
            public BodyDtoValidator()
            {
                RuleFor(x => x.LoadCapacity)
                    .GreaterThan(0).WithName("Грузоподъемность")
                    .WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);
                
                RuleFor(x => x.Length)
                    .GreaterThan(0).WithName("Длина").WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);

                RuleFor(x => x.Width)
                    .GreaterThan(0).WithName("Ширина").WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);

                RuleFor(x => x.Height)
                    .GreaterThan(0).WithName("Высота").WithMessage("Поле '{PropertyName}' должно быть больше 0.")
                    .Unless(x => x is null);

                RuleFor(x => x.Volume)
                    .GreaterThan(0).WithName("Объем").WithMessage("Поле '{PropertyName}' должно быть больше 0.")
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

    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RequestDto, CargoTransport>()
                .MapRecordMember(dest => dest.Images,
                    src => src.Images.ConvertAll(i => new CargoTransportImage{ Url = i }));
            CreateMap<PriceDto, Price>();
            CreateMap<BodyDto, Body>();
        }
    }

    #endregion
}