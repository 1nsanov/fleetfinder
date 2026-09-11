using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport.Special;

namespace FleetFinder.Application.Features.SpecialTransport.GetList;

public static partial class GetSpecialTransportList
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
