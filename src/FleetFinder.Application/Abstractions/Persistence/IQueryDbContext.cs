using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Transport.Special;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Abstractions.Persistence;

public interface IQueryDbContext
{
    DbSet<User> User { get; }
    DbSet<CargoTransport> CargoTransport { get; }
    DbSet<CargoTransportImage> CargoTransportImage { get; }
    DbSet<PassengerTransport> PassengerTransport { get; }
    DbSet<PassengerTransportImage> PassengerTransportImage { get; }
    DbSet<SpecialTransport> SpecialTransport { get; }
    DbSet<SpecialTransportImage> SpecialTransportImage { get; }
}
