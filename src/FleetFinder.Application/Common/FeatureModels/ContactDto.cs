namespace FleetFinder.Application.Common.FeatureModels;

public record ContactDto(
    string Title,
    string? PhoneViber,
    string? PhoneTelegram,
    string? PhoneWhatsapp,
    string? WorkingMode,
    string? ImageUrl
);