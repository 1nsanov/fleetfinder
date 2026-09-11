using FleetFinder.Application.Abstractions.Persistence;
using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Transport.Special;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Tests.Infrastructure;

public sealed class TestCommandDbContext : DbContext, ICommandDbContext
{
    public TestCommandDbContext(DbContextOptions<TestCommandDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null!;
    public DbSet<CargoTransport> CargoTransport { get; set; } = null!;
    public DbSet<CargoTransportImage> CargoTransportImage { get; set; } = null!;
    public DbSet<PassengerTransport> PassengerTransport { get; set; } = null!;
    public DbSet<PassengerTransportImage> PassengerTransportImage { get; set; } = null!;
    public DbSet<SpecialTransport> SpecialTransport { get; set; } = null!;
    public DbSet<SpecialTransportImage> SpecialTransportImage { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.Login).IsUnique();
            e.OwnsOne(x => x.FullName);
            e.OwnsOne(x => x.Contact);
            e.OwnsOne(x => x.RefreshToken);
        });
        modelBuilder.Entity<CargoTransport>(e =>
        {
            e.OwnsOne(x => x.Price);
            e.OwnsOne(x => x.Body);
            e.Navigation(x => x.Images).AutoInclude();
        });
        modelBuilder.Entity<PassengerTransport>(e =>
        {
            e.OwnsOne(x => x.Price);
            e.OwnsOne(x => x.Size);
            e.Navigation(x => x.Images).AutoInclude();
        });
        modelBuilder.Entity<SpecialTransport>(e =>
        {
            e.OwnsOne(x => x.Price);
            e.Navigation(x => x.Images).AutoInclude();
        });
    }
}
