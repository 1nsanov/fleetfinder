using FleetFinder.Domain.Enums.Transport.Passenger;

namespace FleetFinder.Domain.Transport.Passenger;

public class PassengerTransport : TransportBase
{
    public PassengerType Type { get; set; }
    public PassengerRentalDuration? RentalDuration { get; set; }
    public PassengerFacilities? Facilities { get; set; }
    public int? CountSeats { get; set; }
    public Size Size { get; set; } = null!;
    public PassengerOption? Option { get; set; }
    public PassengerTransportationKind? TransportationKind { get; set; }
    public string? Color { get; set; }
    public decimal? MinOrderTime { get; set; }
    public List<PassengerTransportImage> Images { get; set; } = new();
}

public class Size
{
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
}
