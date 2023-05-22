namespace fleetfinder.service.main.application.Common.FeatureModels;

public record ContactDto(
    string Title,
    string? PhoneViber,
    string? PhoneTelegram,
    string? PhoneWhatsapp,
    string? WorkingMode
);