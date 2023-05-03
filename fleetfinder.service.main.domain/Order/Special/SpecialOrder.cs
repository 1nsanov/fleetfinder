using fleetfinder.service.main.domain.Enums.Transport.Special;

namespace fleetfinder.service.main.domain.Order.Special;

public class SpecialOrder : OrderBase
{
    public SpecialType Type { get; set; }

    public List<SpecialOrderImage> Images { get; set; } = new();
}