using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_GetList;

public static partial class CargoTransportGetList
{
    public record ResponseDto(List<CargoTransportDto> Items, int TotalCount);

    public record CargoTransportDto(
        long Id,
        string Title,
        Region Region,
        PriceDto Price,
        string? Description,
        CargoType Type,
        CargoTransportationKind? TransportationKind,
        List<string> Images,
        ContactDto Contact
    );

    public record RequestFilter(
        long? UserFilter,
        string? TitleFilter,
        Region? RegionFilter,
        CargoType? TypeFilter
    );
}