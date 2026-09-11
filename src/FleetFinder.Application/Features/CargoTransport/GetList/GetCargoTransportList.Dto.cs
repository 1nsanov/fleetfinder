using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport.Cargo;

namespace FleetFinder.Application.Features.CargoTransport.GetList;

public static partial class GetCargoTransportList
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
