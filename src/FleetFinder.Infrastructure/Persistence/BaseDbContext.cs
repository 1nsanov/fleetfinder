using FleetFinder.Application.Abstractions.Persistence;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Transport.Special;
using Z.EntityFramework.Plus;

namespace FleetFinder.Infrastructure.Persistence;

public abstract class BaseDbContext : DbContext, IQueryDbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options)
    {
        this.Filter<EntityBase>(q => q.Where(eb => eb.State == State.Actual));
    }

    #region DbSets

    public DbSet<User> User { get; set; } = null!;
    public DbSet<CargoTransport> CargoTransport { get; set; } = null!;
    public DbSet<CargoTransportImage> CargoTransportImage { get; set; } = null!;
    public DbSet<PassengerTransport> PassengerTransport { get; set; } = null!;
    public DbSet<PassengerTransportImage> PassengerTransportImage { get; set; } = null!;
    public DbSet<SpecialTransport> SpecialTransport { get; set; } = null!;
    public DbSet<SpecialTransportImage> SpecialTransportImage { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityBase_Builder(modelBuilder);
        User_Builder(modelBuilder);
        OwnedTypes_Builder(modelBuilder);
        ImagesAutoInclude_Builder(modelBuilder);
    }

    private static void EntityBase_Builder(ModelBuilder modelBuilder)
    {
        var types = modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType.IsAssignableTo(typeof(EntityBase)));

        foreach (var et in types)
        {
            var property = et.FindProperty("CreateDate") ?? throw new NullReferenceException();
            property.SetDefaultValueSql("timezone('utc', current_timestamp)");
            property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;

            property = et.FindProperty("UpdateDate") ?? throw new NullReferenceException();
            property.SetDefaultValueSql("timezone('utc', current_timestamp)");
            property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;

            et.FindProperty("State")?.SetDefaultValueSql("'actual'::state");
        }
    }

    private static void User_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(etp => { etp.HasIndex(u => u.Login).IsUnique(); });
    }

    private static void OwnedTypes_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.OwnsOne(x => x.FullName);
            e.OwnsOne(x => x.Contact);
            e.OwnsOne(x => x.RefreshToken);
        });
        modelBuilder.Entity<CargoTransport>(e =>
        {
            e.OwnsOne(x => x.Price);
            e.OwnsOne(x => x.Body);
        });
        modelBuilder.Entity<PassengerTransport>(e =>
        {
            e.OwnsOne(x => x.Price);
            e.OwnsOne(x => x.Size);
        });
        modelBuilder.Entity<SpecialTransport>(e => e.OwnsOne(x => x.Price));
    }

    private static void ImagesAutoInclude_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargoTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<PassengerTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<SpecialTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
    }
}
