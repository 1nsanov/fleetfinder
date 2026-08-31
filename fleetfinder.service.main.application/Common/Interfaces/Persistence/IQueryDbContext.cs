using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Transport.Special;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;

namespace fleetfinder.service.main.application.Common.Interfaces.Persistence;

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
