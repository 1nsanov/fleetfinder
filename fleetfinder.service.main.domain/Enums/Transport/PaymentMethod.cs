namespace fleetfinder.service.main.domain.Enums.Transport;

public enum PaymentMethod : byte
{
    /// <summary>
    /// Наличный расчет
    /// </summary>
    Cash,
    
    /// <summary>
    /// Безналичный расчет
    /// </summary>
    NonCash,
    
    /// <summary>
    /// Наличный и безналичный расчет
    /// </summary>
    CashAndNonCash,
    
    /// <summary>
    /// Оплата картой
    /// </summary>
    CardPayment
}