namespace fleetfinder.service.main.domain.Transport.Cargo;

public class CargoTransportImage : ImageBase
{
    public CargoTransport Transport { get; set; } = null!;
    public long TransportId { get; set; }
}