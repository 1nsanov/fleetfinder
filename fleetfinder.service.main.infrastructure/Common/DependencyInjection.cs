namespace fleetfinder.service.main.infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContextPool<CommandDbContext>(options =>
        {
            options.UseNpgsql(connectionString ?? "NotFound");
        });
        services.AddDbContextPool<QueryDbContext>(options =>
        {
            options.UseNpgsql(connectionString ?? "NotFound")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
        services.AddDbContext<MigrationDbContext>(options =>
        {
            options.UseNpgsql(connectionString ?? "NotFound", opt =>
            {
                opt.CommandTimeout(1200);
                opt.MigrationsAssembly(typeof(MigrationDbContext).Assembly.GetName().Name);
            });
        });

        return services;
    }
}