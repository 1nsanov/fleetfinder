﻿using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.domain.Bases;

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

[Owned]
public class Price
{
    public decimal? PerHour { get; set; }
    public decimal? PerShift { get; set; }
    public decimal? PerKm { get; set; }
}