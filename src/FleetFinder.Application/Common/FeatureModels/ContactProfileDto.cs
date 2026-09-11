namespace FleetFinder.Application.Common.FeatureModels;

public record ContactProfileDto(
    string? PhoneViber,
    string? PhoneTelegram,
    string? PhoneWhatsapp,
    string? WorkingMode
);