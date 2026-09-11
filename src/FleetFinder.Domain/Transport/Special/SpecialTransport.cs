using FleetFinder.Domain.Enums.Transport.Special;

namespace FleetFinder.Domain.Transport.Special;

public class SpecialTransport : TransportBase
{
    public SpecialType Type { get; set; }
    
    public List<SpecialTransportImage> Images { get; set; } = new();
}