namespace fleetfinder.service.main.domain.Transport.Passenger;

public class PassengerTransportImage : ImageBase
{
    public PassengerTransport Transport { get; set; } = null!;
    public long TransportId { get; set; }
}