using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Enums.Transport.Special;

namespace FleetFinder.Application.Features.SpecialTransport.Update;

public static partial class UpdateSpecialTransport
{
    public record RequestDto(
        long Id,
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
