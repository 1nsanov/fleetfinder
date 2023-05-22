namespace fleetfinder.service.main.application.Common.FeatureModels;

public record PriceDto(
    decimal? PerHour,
    decimal? PerShift,
    decimal? PerKm
);