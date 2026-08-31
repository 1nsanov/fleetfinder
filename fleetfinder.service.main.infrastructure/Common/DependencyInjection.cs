using Amazon.S3;
using fleetfinder.service.main.application.Common.Interfaces.Persistence;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;
using fleetfinder.service.main.domain.Enums.Transport.Special;
using fleetfinder.service.main.infrastructure.Identity;
using fleetfinder.service.main.infrastructure.Storage;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

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
            options.UseNpgsql(connectionString ?? "NotFound", ConfigureNpgsql);
        });
        services.AddScoped<ICommandDbContext>(sp => sp.GetRequiredService<CommandDbContext>());
        services.AddDbContextPool<QueryDbContext>(options =>
        {
            options.UseNpgsql(connectionString ?? "NotFound", ConfigureNpgsql)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
        services.AddScoped<IQueryDbContext>(sp => sp.GetRequiredService<QueryDbContext>());
        services.AddDbContext<MigrationDbContext>(options =>
        {
            options.UseNpgsql(connectionString ?? "NotFound", opt =>
            {
                ConfigureNpgsql(opt);
                opt.CommandTimeout(1200);
                opt.MigrationsAssembly(typeof(MigrationDbContext).Assembly.GetName().Name);
            });
        });

        services.Configure<S3StorageOptions>(configuration.GetSection(S3StorageOptions.SectionName));
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<S3StorageOptions>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "us-east-1"
            };
            return new AmazonS3Client(options.AccessKey, options.SecretKey, config);
        });
        services.AddSingleton<IObjectStorageService, S3ObjectStorageService>();

        services.AddJwtAuthentication(configuration);

        return services;
    }

    private static void ConfigureNpgsql(NpgsqlDbContextOptionsBuilder options)
    {
        options.MapEnum<State>("state");
        options.MapEnum<Region>("region");
        options.MapEnum<ExperienceWork>("experience_work");
        options.MapEnum<PaymentMethod>("payment_method");
        options.MapEnum<PaymentOrder>("payment_order");
        options.MapEnum<CargoType>("cargo_type");
        options.MapEnum<CargoBodyKind>("cargo_body_kind");
        options.MapEnum<CargoTransportationKind>("cargo_transportation_kind");
        options.MapEnum<PassengerType>("passenger_type");
        options.MapEnum<PassengerRentalDuration>("passenger_rental_duration");
        options.MapEnum<PassengerFacilities>("passenger_facilities");
        options.MapEnum<PassengerOption>("passenger_option");
        options.MapEnum<PassengerTransportationKind>("passenger_transportation_kind");
        options.MapEnum<SpecialType>("special_type");
    }

    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
        context.Database.Migrate();
    }
}
