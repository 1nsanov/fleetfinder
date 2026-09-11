using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Enums.Transport.Passenger;

namespace FleetFinder.Application.Features.PassengerTransport.Update;

public static partial class UpdatePassengerTransport
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
