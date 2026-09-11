using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Enums.Transport.Passenger;

namespace FleetFinder.Application.Features.PassengerTransport.Get;

public static partial class GetPassengerTransport
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
