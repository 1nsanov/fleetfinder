namespace fleetfinder.service.main.domain.Order.Passenger;

public class PassengerOrderImage : ImageBase
{
    public PassengerOrder Order { get; set; } = null!;
    public long OrderId { get; set; }
}