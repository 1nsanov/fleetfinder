namespace fleetfinder.service.main.domain.Enums.Order.Cargo;

public enum CargoLoadType : byte
{
    /// <summary>
    /// Верхняя
    /// </summary>
    Top,
    /// <summary>
    /// Задняя
    /// </summary>
    Rear,
    /// <summary>
    /// Боковая
    /// </summary>
    Side,
    /// <summary>
    /// С гидробортом
    /// </summary>
    WithHydroboard,
    /// <summary>
    /// С полной растентовкой
    /// </summary>
    WithFullLiftgate,
    /// <summary>
    /// С аппарелями /сходнями
    /// </summary>
    WithRampsOrDischarges,
    /// <summary>
    /// С обрешеткой
    /// </summary>
    WithTarmac,
    /// <summary>
    /// С кониками
    /// </summary>
    WithStakes,
    /// <summary>
    /// С пневмоподвеской
    /// </summary>
    WithAirSuspension,
}