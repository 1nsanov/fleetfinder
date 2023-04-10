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

    public DbSet<User> Users { get; set; } = null!;

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
    }
    
    private static void Enums_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<State>();
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
            etp.HasIndex(u => u.Login);
        });
    }
}