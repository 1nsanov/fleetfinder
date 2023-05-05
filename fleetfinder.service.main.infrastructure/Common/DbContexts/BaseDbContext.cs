using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Order.Cargo;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;
using fleetfinder.service.main.domain.Enums.Transport.Special;
using fleetfinder.service.main.domain.Order.Cargo;
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
        this.Filter<EntityBase>(
            q => q.Where(eb => eb.State == State.Actual));
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
    public DbSet<SpecialOrder> SpecialOrder { get; set; } = null!;
    public DbSet<SpecialOrderImage> SpecialOrderImage { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityBase_Builder(modelBuilder);
        Enums_Builder(modelBuilder);
        User_Builder(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #region Enums

    // Для работы Enum-ов необходимо добавление в конструктор и метод
    // Имена Enum-ов должны быть уникальны
    static BaseDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<State>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Region>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ExperienceWork>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PaymentMethod>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PaymentOrder>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoBodyKind>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoTransportationKind>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoLoaders>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoLoadType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CargoTransportationType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PassengerType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PassengerRentalDuration>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PassengerFacilities>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PassengerOption>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PassengerTransportationKind>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<SpecialType>();
    }
    
    private static void Enums_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<State>();
        modelBuilder.HasPostgresEnum<Region>();
        modelBuilder.HasPostgresEnum<ExperienceWork>();
        modelBuilder.HasPostgresEnum<PaymentMethod>();
        modelBuilder.HasPostgresEnum<PaymentOrder>();
        modelBuilder.HasPostgresEnum<CargoType>();
        modelBuilder.HasPostgresEnum<CargoBodyKind>();
        modelBuilder.HasPostgresEnum<CargoTransportationKind>();
        modelBuilder.HasPostgresEnum<CargoLoaders>();
        modelBuilder.HasPostgresEnum<CargoLoadType>();
        modelBuilder.HasPostgresEnum<CargoTransportationType>();
        modelBuilder.HasPostgresEnum<PassengerType>();
        modelBuilder.HasPostgresEnum<PassengerRentalDuration>();
        modelBuilder.HasPostgresEnum<PassengerFacilities>();
        modelBuilder.HasPostgresEnum<PassengerOption>();
        modelBuilder.HasPostgresEnum<PassengerTransportationKind>();
        modelBuilder.HasPostgresEnum<SpecialType>();
    }

    #endregion

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
        modelBuilder.Entity<User>(etp =>
        {
            etp.HasIndex(u => u.Login).IsUnique();
        });
    }
}