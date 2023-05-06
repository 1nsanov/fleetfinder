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
        // string? Brand,
        // DateOnly? YearIssue,
        // ExperienceWork? ExperienceWork,
        // PaymentMethod?  PaymentMethod,
        // PaymentOrder? PaymentOrder,
        PriceDto Price,
        string? Description,
        CargoType Type,
        // BodyDto Body,
        // CargoTransportationKind? TransportationKind,
        List<string> Images,
        ContactDto Contact
    );

    public record PriceDto(
        decimal? PerHour,
        decimal? PerShift,
        decimal? PerKm
    );
    
    public record ContactDto(
        string Title,
        string? PhoneViber,
        string? PhoneTelegram,
        string? PhoneWhatsapp,
        string? WorkingMode
    );

    public record RequestFilter(
        long? UserFilter,
        string? TitleFilter,
        Region? RegionFilter,
        CargoType? TypeFilter
    );
}