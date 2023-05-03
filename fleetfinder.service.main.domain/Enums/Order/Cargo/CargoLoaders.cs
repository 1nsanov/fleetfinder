namespace fleetfinder.service.main.domain.Enums.Order.Cargo;

public enum CargoLoaders : byte
{
    /// <summary>
    /// Без грузчиков
    /// </summary>
    WithoutMovers,
    /// <summary>
    /// Помощь одного грузчика
    /// </summary>
    OneMovers,
    /// <summary>
    /// Помощь двух грузчиков
    /// </summary>
    TwoMovers,
    /// <summary>
    /// Помощь трёх грузчиков
    /// </summary>
    ThreeMovers,
}