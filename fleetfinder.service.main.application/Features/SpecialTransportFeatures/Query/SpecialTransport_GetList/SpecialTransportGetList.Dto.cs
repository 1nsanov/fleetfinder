using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Special;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_GetList;

public static partial class SpecialTransportGetList
{
    public record ResponseDto(List<SpecialTransportDto> Items, int TotalCount);

    public record SpecialTransportDto(
        long Id,
        string Title,
        Region Region,
        PriceDto Price,
        string? Description,
        SpecialType Type,
        List<string> Images,
        ContactDto Contact
    );

    public record RequestFilter(
        long? UserFilter,
        string? TitleFilter,
        Region? RegionFilter,
        SpecialType? TypeFilter
    );
}