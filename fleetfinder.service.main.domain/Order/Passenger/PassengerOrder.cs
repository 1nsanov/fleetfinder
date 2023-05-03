using fleetfinder.service.main.domain.Enums.Transport.Passenger;

namespace fleetfinder.service.main.domain.Order.Passenger;

public class PassengerOrder : OrderBase
{
    public PassengerType Type { get; set; }
    public PassengerRentalDuration? RentalDuration { get; set; }
    public PassengerFacilities? Facilities { get; set; }
    public int? CountSeats { get; set; }
    public PassengerOption? Option { get; set; }
    public PassengerTransportationKind? TransportationKind { get; set; }
    
    
    public List<PassengerOrderImage> Images { get; set; } = null!;
}