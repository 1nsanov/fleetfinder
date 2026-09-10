using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Cargo;


namespace FleetFinder.Application.Features.CargoTransport.Create;

public static partial class CreateCargoTransport
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
                RuleFor(x => x.Title)
                    .NotEmpty().WithName("Title").WithMessage(ValidationMessages.NotEmpty)
                    .MaximumLength(100).WithName("Title")
                    .WithMessage(ValidationMessages.MaxLength);

                RuleFor(x => x.Brand)
                    .MaximumLength(50).WithName("Brand")
                    .WithMessage(ValidationMessages.MaxLength)
                    .Unless(x => string.IsNullOrEmpty(x.Brand));

                RuleFor(x => x.Price)
                    .SetValidator(new PriceDtoValidator());

                RuleFor(x => x.Description)
                    .MaximumLength(1000).WithName("Description")
                    .WithMessage(ValidationMessages.MaxLength)
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
                    .GreaterThan(0).WithName("Price per hour").WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);

                RuleFor(x => x.PerShift)
                    .GreaterThan(0).WithName("Price per shift")
                    .WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);

                RuleFor(x => x.PerKm)
                    .GreaterThan(0).WithName("Price per kilometer")
                    .WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);
            }
        }

        public class BodyDtoValidator : AbstractValidator<BodyDto>
        {
            public BodyDtoValidator()
            {
                RuleFor(x => x.LoadCapacity)
                    .GreaterThan(0).WithName("Load capacity")
                    .WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);
                
                RuleFor(x => x.Length)
                    .GreaterThan(0).WithName("Length").WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);

                RuleFor(x => x.Width)
                    .GreaterThan(0).WithName("Width").WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);

                RuleFor(x => x.Height)
                    .GreaterThan(0).WithName("Height").WithMessage(ValidationMessages.GreaterThanZero)
                    .Unless(x => x is null);

                RuleFor(x => x.Volume)
                    .GreaterThan(0).WithName("Volume").WithMessage(ValidationMessages.GreaterThanZero)
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
                        image.NotEmpty().WithMessage(ValidationMessages.ImageUrlRequired)
                            .Matches(@"^https?://[^\s/$.?#].[^\s]*$").WithMessage(ValidationMessages.ImageUrlInvalid);
                    })
                    .Unless(x => x.Count == 0);
            }
        }
    }

    
}
