namespace fleetfinder.service.main.domain.Order.Cargo;

public class CargoOrderImage : ImageBase
{
    public CargoOrder Order { get; set; } = null!;
    public long OrderId { get; set; }
}