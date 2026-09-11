namespace FleetFinder.Application.Common.Seed;

public interface IDemoDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
