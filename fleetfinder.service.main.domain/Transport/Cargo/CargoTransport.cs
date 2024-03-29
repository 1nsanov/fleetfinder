﻿using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.domain.Transport.Cargo;

public class CargoTransport : TransportBase
{
    public CargoType Type { get; set; }
    public Body Body { get; set; } = null!;
    public CargoTransportationKind? TransportationKind { get; set; }
    
    
    public List<CargoTransportImage> Images { get; set; } = new();
}

[Owned]
public class Body
{
    public decimal? LoadCapacity { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Volume { get; set; }
    public CargoBodyKind? Kind { get; set; }
}