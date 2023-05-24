using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_Get;

public static partial class PassengerTransportGet
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
        PassengerType Type,
        List<string> Images,
        PassengerRentalDuration? RentalDuration,
        PassengerFacilities? Facilities,
        int? CountSeats,
        SizeDto Size,
        PassengerOption? Option,
        PassengerTransportationKind? TransportationKind,
        string? Color,
        decimal? MinOrderTime,
        ContactDto Contact,
        DateOnly CreateDate,
        long UserId
    );
}