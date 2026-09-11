using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Enums.Transport.Cargo;

namespace FleetFinder.Application.Features.CargoTransport.Get;

public static partial class GetCargoTransport
{
    public record ResponseDto(
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
        CargoType Type,
        BodyDto Body,
        CargoTransportationKind? TransportationKind,
        List<string> Images,
        ContactDto Contact,
        DateOnly CreateDate,
        long UserId
    );
}
