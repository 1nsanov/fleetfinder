using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Enums.Transport.Special;

namespace FleetFinder.Application.Features.SpecialTransport.Create;

public static partial class CreateSpecialTransport
{
    public record RequestDto(
        string Title,
        Region Region,
        string? Brand,
        string? YearIssue,
        ExperienceWork? ExperienceWork,
        PaymentMethod?  PaymentMethod,
        PaymentOrder? PaymentOrder,
        PriceDto Price,
        string? Description,
        SpecialType Type,
        List<string> Images
    );

    public record ResponseDto(long Id);
}
