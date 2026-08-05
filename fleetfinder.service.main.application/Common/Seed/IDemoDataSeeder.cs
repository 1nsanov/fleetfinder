namespace fleetfinder.service.main.application.Common.Seed;

public interface IDemoDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
