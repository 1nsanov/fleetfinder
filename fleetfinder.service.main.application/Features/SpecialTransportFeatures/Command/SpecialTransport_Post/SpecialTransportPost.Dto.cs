using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Special;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Command.SpecialTransport_Post;

public static partial class SpecialTransportPost
{
    public record RequestDto(
        string Title,
        Region Region,
        string? Brand,
        string? YearIssue,
        ExperienceWork? ExperienceWork,
        PaymentMethod?  PaymentMethod,
        PaymentOrder? PaymentOrder,
        PriceDto Price,
        string? Description,
        SpecialType Type,
        List<string> Images
    );

    public record ResponseDto(long Id);
}