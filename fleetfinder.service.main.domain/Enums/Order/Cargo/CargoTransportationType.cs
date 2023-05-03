namespace fleetfinder.service.main.domain.Enums.Order.Cargo;

public enum CargoTransportationType : byte
{
    /// <summary>
    /// Полная загрузка
    /// </summary>
    FullLoad,
    /// <summary>
    /// Частичная загрузка (догруз)
    /// </summary>
    PartialLoad,
    /// <summary>
    /// Негабаритная (большегруз)
    /// </summary>
    Overload
}