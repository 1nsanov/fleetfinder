namespace fleetfinder.service.main.domain.Enums.Transport.Passenger;

public enum PassengerTransportationKind : byte
{
    /// <summary>
    /// Заказные перевозки
    /// </summary>
    Order,
    /// <summary>
    /// Перевозка детей
    /// </summary>
    Children,
    /// <summary>
    /// Перевозка туристов
    /// </summary>
    Tourists,
    /// <summary>
    /// Междугородние перевозки
    /// </summary>
    Intercity,
    /// <summary>
    /// Поездки за границу
    /// </summary>
    AbroadTrips,
    /// <summary>
    /// Корпоративные перевозки
    /// </summary>
    Corporate,
    /// <summary>
    /// Трансфер в аэропорт
    /// </summary>
    AirportTransfer,
    /// <summary>
    /// Доставка сотрудников
    /// </summary>
    EmployeeDelivery,
    /// <summary>
    /// Свадебные перевозки
    /// </summary>
    Wedding,
    /// <summary>
    /// VIP перевозки
    /// </summary>
    VIP,
    /// <summary>
    /// Перевозка больных (медицинский)
    /// </summary>
    Medical,
    /// <summary>
    /// Ритуальные перевозки
    /// </summary>
    Funeral,
    /// <summary>
    /// Зоотакси (перевозка животных)
    /// </summary>
    Pet,
    /// <summary>
    /// Автодом
    /// </summary>
    CarHome,
    /// <summary>
    /// Экскурсионный
    /// </summary>
    Excursion,
    /// <summary>
    /// Пати бас
    /// </summary>
    PartyBus,
    /// <summary>
    /// курьерские услуги
    /// </summary>
    Courier 
}