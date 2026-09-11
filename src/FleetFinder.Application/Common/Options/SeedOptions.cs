namespace FleetFinder.Application.Common.Options;

public class SeedOptions
{
    public const string SectionName = "Seed";

    public bool Enabled { get; set; }

    public string DemoPassword { get; set; } = "Demo123!";
}
