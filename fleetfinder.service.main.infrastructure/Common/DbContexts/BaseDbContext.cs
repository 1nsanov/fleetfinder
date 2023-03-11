namespace fleetfinder.service.main.infrastructure.Common.DbContexts;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    #region DbSets

    public DbSet<User> Users { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityBase_Builder(modelBuilder);
        Enums_Builder(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #region Enums

    // Для работы Enum-ов необходимо добавление в конструктор и метод
    // Имена Enum-ов должны быть уникальны
    static BaseDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<State>();
    }
    
    private void Enums_Builder(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<State>();
    }

    #endregion

    private void EntityBase_Builder(ModelBuilder modelBuilder)
    {
        foreach (var et in modelBuilder.Model.GetEntityTypes())
        {
            if (et.ClrType.IsSubclassOf(typeof(EntityBase)))
            {
                var property = et.FindProperty("CreateDate") ?? throw new NullReferenceException();
                property.SetDefaultValueSql("timezone('utc', current_timestamp)");
                property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;

                property = et.FindProperty("UpdateDate") ?? throw new NullReferenceException();
                property.SetDefaultValueSql("timezone('utc', current_timestamp)");
                property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
                et.FindProperty("State")?.SetDefaultValue(State.Actual);
            }
        }
    }
}