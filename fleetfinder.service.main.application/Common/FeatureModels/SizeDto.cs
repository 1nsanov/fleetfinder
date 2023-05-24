namespace fleetfinder.service.main.application.Common.FeatureModels;

public record SizeDto(
    decimal? Length,
    decimal? Width,
    decimal? Height
);