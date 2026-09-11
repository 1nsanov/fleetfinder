namespace FleetFinder.Application.Common.Options;

public class PasswordOptions
{
    public const string SectionName = "Password";

    public int MemorySize { get; set; } = 19456;
    public int Iterations { get; set; } = 2;
    public int Parallelism { get; set; } = 1;
    public int SaltSize { get; set; } = 16;
    public int HashSize { get; set; } = 32;
}
