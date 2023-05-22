using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.application.Common.FeatureModels;

public record BodyDto(
    decimal? LoadCapacity,
    decimal? Length,
    decimal? Width,
    decimal? Height,
    decimal? Volume,
    CargoBodyKind? Kind
);