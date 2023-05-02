namespace fleetfinder.service.main.domain.Transport.Special;

public class SpecialTransportImage : ImageBase
{
    public SpecialTransport Transport { get; set; } = null!;
    public long TransportId { get; set; }
}