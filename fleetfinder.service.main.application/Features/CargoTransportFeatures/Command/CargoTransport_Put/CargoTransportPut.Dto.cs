using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Put;

public static partial class CargoTransportPut
{
    public record RequestDto(
        long Id,
        string Title,
        Region Region,
        string? Brand,
        DateOnly? YearIssue,
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

    public record PriceDto(
        decimal? PerHour,
        decimal? PerShift,
        decimal? PerKm
    );

    public record BodyDto(
        decimal? LoadCapacity,
        decimal? Length,
        decimal? Width,
        decimal? Height,
        decimal? Volume,
        CargoBodyKind? Kind
    );

    public record ResponseDto(long Id);
}