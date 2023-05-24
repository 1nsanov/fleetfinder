using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_GetList;

public static partial class PassengerTransportGetList
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