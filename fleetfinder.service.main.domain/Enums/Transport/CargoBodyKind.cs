namespace fleetfinder.service.main.domain.Enums.Transport;

public enum CargoBodyKind : byte
{
    /// <summary>
    /// Автопоезд
    /// </summary>
    AutoTrain,
    
    /// <summary>
    /// Балковоз
    /// </summary>
    BeamVehicle,
    
    /// <summary>
    /// Бортовой
    /// </summary>
    FlatbedVehicle,
    
    /// <summary>
    /// Гидроборт
    /// </summary>
    HydraulicLift,
    
    /// <summary>
    /// Грузопассажирский
    /// </summary>
    CargoPassenger,
    
    /// <summary>
    /// Длинномер
    /// </summary>
    OversizedVehicle,
    
    /// <summary>
    /// Изотермический
    /// </summary>
    IsothermalVehicle,
    
    /// <summary>
    /// Контейнеровоз
    /// </summary>
    ContainerVehicle,
    
    /// <summary>
    /// Кормовоз
    /// </summary>
    FeedVehicle,
    
    /// <summary>
    /// Муковоз
    /// </summary>
    FlourVehicle,
    
    /// <summary>
    /// Открытый
    /// </summary>
    OpenVehicle,
    
    /// <summary>
    /// Панелевоз
    /// </summary>
    PanelVehicle,
    
    /// <summary>
    /// Пикап
    /// </summary>
    Pickup,
    
    /// <summary>
    /// Пирамида
    /// </summary>
    Pyramid,
    
    /// <summary>
    /// Полуприцеп
    /// </summary>
    SemiTrailer,
    
    /// <summary>
    /// Прицеп легковой
    /// </summary>
    LightTrailer,
    
    /// <summary>
    /// Рефрижератор
    /// </summary>
    Refrigerator,
    
    /// <summary>
    /// Рулоновоз
    /// </summary>
    RollVehicle,
    
    /// <summary>
    /// Седельный тягач
    /// </summary>
    TractorUnit,
    
    /// <summary>
    /// Сельхозник/зерновоз
    /// </summary>
    AgriculturalGrainVehicle,
    
    /// <summary>
    /// Скотовоз
    /// </summary>
    LivestockVehicle,
    
    /// <summary>
    /// Сортиментовоз/лесовоз
    /// </summary>
    TimberVehicle,
    
    /// <summary>
    /// Стекловоз
    /// </summary>
    GlassVehicle,
    
    /// <summary>
    /// Сцепка
    /// </summary>
    Coupling,
    
    /// <summary>
    /// Танк-контейнер
    /// </summary>
    TankContainer,
    
    /// <summary>
    /// Тентованный
    /// </summary>
    CurtainSider,
    
    /// <summary>
    /// Термобудка
    /// </summary>
    InsulatedVehicle,
    
    /// <summary>
    /// Трубовоз
    /// </summary>
    PipeVehicle,
    
    /// <summary>
    /// Фура
    /// </summary>
    TrailerTruck,
    
    /// <summary>
    /// Фургон
    /// </summary>
    Van,
    
    /// <summary>
    /// Цельнометаллический
    /// </summary>
    SolidMetal,
    
    /// <summary>
    /// Щеповоз
    /// </summary>
    ChipCar
}