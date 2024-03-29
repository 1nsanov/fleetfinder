﻿using fleetfinder.service.main.domain.Order.Cargo;
using fleetfinder.service.main.domain.Order.Passenger;
using fleetfinder.service.main.domain.Order.Special;
using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Transport.Special;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.domain.Users;

public class User : EntityBase
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public FullName FullName { get; set; } = new();
    public string? Organization { get; set; }
    public string? ImageUrl { get; set; }
    public Contact Contact { get; set; } = null!;
    public RefreshToken? RefreshToken { get; set; } = new();
    
    //Relations
    public List<CargoTransport> CargoTransports { get; set; } = new();
    public List<PassengerTransport> PassengerTransports  { get; set; } = new();
    public List<SpecialTransport> SpecialTransports { get; set; } = new();
    public List<CargoOrder> CargoOrders { get; set; } = new();
    public List<PassengerOrder> PassengerOrders { get; set; } = new();
    public List<SpecialOrder> SpecialOrders { get; set; } = new();
}

[Owned]
public class RefreshToken
{
    public string? Value { get; set; }
    public DateTime? ExpiryTime { get; set; }
}

[Owned]
public class FullName
{
    public string First { get; set; } = null!;
    public string Second { get; set; } = null!;
    public string? Surname { get; set; }
}