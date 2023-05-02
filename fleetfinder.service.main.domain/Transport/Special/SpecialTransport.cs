using fleetfinder.service.main.domain.Enums.Transport.Special;

namespace fleetfinder.service.main.domain.Transport.Special;

public class SpecialTransport : TransportBase
{
    public SpecialType Type { get; set; }
    
    public List<SpecialTransportImage> Images { get; set; } = new();
}