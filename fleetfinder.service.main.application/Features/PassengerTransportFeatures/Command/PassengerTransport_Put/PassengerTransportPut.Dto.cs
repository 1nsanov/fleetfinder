using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Command.PassengerTransport_Put;

public static partial class PassengerTransportPut
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
        PassengerType Type,
        List<string> Images,
        PassengerRentalDuration? RentalDuration,
        PassengerFacilities? Facilities,
        int? CountSeats,
        SizeDto Size,
        PassengerOption? Option,
        PassengerTransportationKind? TransportationKind,
        string? Color,
        decimal? MinOrderTime
    );

    public record ResponseDto(long Id);
}