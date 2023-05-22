using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Post;

public static partial class CargoTransportPost
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
        CargoType Type,
        BodyDto Body,
        CargoTransportationKind? TransportationKind,
        List<string> Images
    );

    public record ResponseDto(long Id);
}