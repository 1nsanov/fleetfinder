using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_Get;

public static partial class CargoTransportGet
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