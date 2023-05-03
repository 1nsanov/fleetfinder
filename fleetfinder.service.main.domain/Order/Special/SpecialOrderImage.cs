namespace fleetfinder.service.main.domain.Order.Special;

public class SpecialOrderImage : ImageBase
{
    public SpecialOrder Order { get; set; } = null!;
    public long OrderId { get; set; }
}