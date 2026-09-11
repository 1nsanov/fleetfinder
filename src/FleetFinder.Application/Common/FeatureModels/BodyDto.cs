using FleetFinder.Domain.Enums.Transport.Cargo;

namespace FleetFinder.Application.Common.FeatureModels;

public record BodyDto(
    decimal? LoadCapacity,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? Volume,
    CargoBodyKind? Kind
);