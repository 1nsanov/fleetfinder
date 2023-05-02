namespace fleetfinder.service.main.domain.Enums;

public enum PaymentOrder : byte
{
    /// <summary>
    /// Предоплата
    /// </summary>
    Prepayment,
    
    /// <summary>
    /// Оплата по факту
    /// </summary>
    PaymentUponDelivery,
    
    /// <summary>
    /// Поэтапная оплата
    /// </summary>
    InstallmentPayment
}