using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport.Passenger;

namespace FleetFinder.Application.Features.PassengerTransport.GetList;

public static partial class GetPassengerTransportList
{
    public record ResponseDto(List<PassengerTransportDto> Items, int TotalCount);

    public record PassengerTransportDto(
        long Id,
        string Title,
        Region Region,
        PriceDto Price,
        string? Description,
        PassengerType Type,
        List<string> Images,
        PassengerFacilities? Facilities,
        PassengerTransportationKind? TransportationKind,
        ContactDto Contact
    );

    public record RequestFilter(
        long? UserFilter,
        string? TitleFilter,
        Region? RegionFilter,
        PassengerType? TypeFilter
    );
}
