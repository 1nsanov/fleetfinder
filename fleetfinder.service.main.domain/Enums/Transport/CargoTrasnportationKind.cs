namespace fleetfinder.service.main.domain.Enums.Transport;

public enum CargoTransportationKind : byte
{
    /// <summary>
    /// Грузовое такси
    /// </summary>
    CargoTaxi,
    
    /// <summary>
    /// Квартирный переезд
    /// </summary>
    ApartmentMoving,
    
    /// <summary>
    /// Офисный переезд
    /// </summary>
    OfficeMoving,
    
    /// <summary>
    /// Перевозка мебели
    /// </summary>
    FurnitureTransport,
    
    /// <summary>
    /// Перевозка продуктов
    /// </summary>
    FoodTransport,
    
    /// <summary>
    /// Междугородние перевозки
    /// </summary>
    IntercityTransport,
    
    /// <summary>
    /// Международные перевозки
    /// </summary>
    InternationalTransport,
    
    /// <summary>
    /// Доставка сборных грузов
    /// </summary>
    LCLTransport,
    
    /// <summary>
    /// Перевозка лошадей и крупного рогатого скота
    /// </summary>
    LivestockTransport,
    
    /// <summary>
    /// Перевозка личных вещей
    /// </summary>
    PersonalItemsTransport,
    
    /// <summary>
    /// Перевозка бытовой техники
    /// </summary>
    ApplianceTransport,
    
    /// <summary>
    /// Перевозка фруктов
    /// </summary>
    FruitTransport,
    
    /// <summary>
    /// Перевозка овощей
    /// </summary>
    VegetableTransport,
    
    /// <summary>
    /// Дачный переезд
    /// </summary>
    CountryHouseMoving,
    
    /// <summary>
    /// Переезд склада
    /// </summary>
    WarehouseMoving,
    
    /// <summary>
    /// Перевозка пианино
    /// </summary>
    PianoTransport,
    
    /// <summary>
    /// Перевозка сейфов
    /// </summary>
    SafeTransport,
    
    /// <summary>
    /// Перевозка холодильников
    /// </summary>
    RefrigeratorTransport,
    
    /// <summary>
    /// Перевозка оборудования
    /// </summary>
    EquipmentTransport,
    
    /// <summary>
    /// Перевозка строительных материалов
    /// </summary>
    ConstructionMaterialsTransport,
    
    /// <summary>
    /// Перевозка мотоциклов
    /// </summary>
    MotorcycleTransport
}