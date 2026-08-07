using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Order.Cargo;
using fleetfinder.service.main.domain.Order.Passenger;
using fleetfinder.service.main.domain.Order.Special;
using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Transport.Special;
using Z.EntityFramework.Plus;

namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public abstract class BaseDbContext : DbContext
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
    public DbSet<CargoOrder> CargoOrder { get; set; } = null!;
    public DbSet<CargoOrderImage> CargoOrderImage { get; set; } = null!;
    public DbSet<PassengerOrder> PassengerOrder { get; set; } = null!;
    public DbSet<PassengerOrderImage> PassengerOrderImage { get; set; } = null!;
    public DbSet<SpecialOrder> SpecialOrder { get; set; } = null!;
    public DbSet<SpecialOrderImage> SpecialOrderImage { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityBase_Builder(modelBuilder);
        User_Builder(modelBuilder);
        ImagesAutoInclude_Builder(modelBuilder);
    }

    private static void EntityBase_Builder(ModelBuilder modelBuilder)
    {
        var types = modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType.IsAssignableTo(typeof(EntityBase)));

        // configuration for all derived entities
        foreach (var et in types)
        {
            //default value
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

    private static void ImagesAutoInclude_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargoTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<PassengerTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<SpecialTransport>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<CargoOrder>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<PassengerOrder>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
        modelBuilder.Entity<SpecialOrder>(etp => { etp.Navigation(x => x.Images).AutoInclude(); });
    }
}