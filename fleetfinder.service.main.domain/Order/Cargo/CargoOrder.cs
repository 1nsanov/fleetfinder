using fleetfinder.service.main.domain.Enums.Order.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;

namespace fleetfinder.service.main.domain.Order.Cargo;

public class CargoOrder : OrderBase
{
    public CargoType Type { get; set; }
    public DateOnly ShipmentDate { get; set; }
    public CargoLoaders? Loaders { get; set; }
    public CargoBodyKind? BodyKind { get; set; }
    public CargoTransportationType? TransportationType { get; set; }
    public CargoLoadType? LoadType { get; set; }


    public List<CargoOrderImage> Images { get; set; } = new();
}