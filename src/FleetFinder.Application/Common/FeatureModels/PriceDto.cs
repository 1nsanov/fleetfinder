namespace FleetFinder.Application.Common.FeatureModels;

public record PriceDto(
    decimal? PerHour,
    decimal? PerShift,
    decimal? PerKm
);