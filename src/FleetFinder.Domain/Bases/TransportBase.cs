using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport;
using FleetFinder.Domain.Users;

namespace FleetFinder.Domain.Bases;

public class TransportBase : EntityBase
{
    public string Title { get; set; } = null!;
    public Region Region { get; set; }
    public string? Brand { get; set; }
    public string? YearIssue { get; set; }
    public ExperienceWork? ExperienceWork { get; set; }
    public PaymentMethod?  PaymentMethod { get; set; }
    public PaymentOrder? PaymentOrder { get; set; }
    public Price Price { get; set; } = null!;
    public string? Description { get; set; }
    public User User { get; set; } = null!;
    public long UserId { get; set; }
}

public class Price
{
    public decimal? PerHour { get; set; }
    public decimal? PerShift { get; set; }
    public decimal? PerKm { get; set; }
}
