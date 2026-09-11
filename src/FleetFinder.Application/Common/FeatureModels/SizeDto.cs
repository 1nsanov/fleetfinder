namespace FleetFinder.Application.Common.FeatureModels;

public record SizeDto(
    decimal? Length,
    decimal? Width,
    decimal? Height
);