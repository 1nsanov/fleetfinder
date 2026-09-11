namespace FleetFinder.Domain.Transport.Passenger;

public class PassengerTransportImage : ImageBase
{
    public PassengerTransport Transport { get; set; } = null!;
    public long TransportId { get; set; }
}