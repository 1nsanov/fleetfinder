using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.domain.Bases;

public class OrderBase : EntityBase
{
    public string Title { get; set; } = null!;
    public Region PickupRegion { get; set; }
    public Region DeliverRegion { get; set; }
    public decimal MaxBudget { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? Description { get; set; }


    public User User { get; set; } = null!;
    public long UserId { get; set; }
}