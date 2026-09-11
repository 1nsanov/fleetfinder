using FleetFinder.Application.Common.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FleetFinder.Application.Common.Seed;

public static class SeedExtensions
{
    public static async Task SeedDemoDataAsync(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<SeedOptions>>().Value;
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DemoDataSeed");

        if (!options.Enabled)
        {
            logger.LogInformation("Demo seed disabled (Seed:Enabled=false)");
            return;
        }

        var seeder = scope.ServiceProvider.GetRequiredService<IDemoDataSeeder>();
        await seeder.SeedAsync(cancellationToken);
    }
}
