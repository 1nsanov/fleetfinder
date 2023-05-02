namespace fleetfinder.service.main.domain.Enums.Transport.Passenger;

public enum PassengerRentalDuration : byte
{
    /// <summary>
    /// На сутки
    /// </summary>
    PerDay,
    
    /// <summary>
    /// На день
    /// </summary>
    OneDay,
    
    /// <summary>
    /// На час
    /// </summary>
    PerHour,
    
    /// <summary>
    /// На месяц
    /// </summary>
    PerMonth,
    
    /// <summary>
    /// Долгосрочная
    /// </summary>
    LongTerm
}