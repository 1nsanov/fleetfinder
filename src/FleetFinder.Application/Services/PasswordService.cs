using System.Security.Cryptography;
using System.Text;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Application.Common.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace FleetFinder.Application.Services;

public class PasswordService : IPasswordService
{
    private const string AlgorithmId = "argon2id";
    private const int Version = 19;

    private readonly PasswordOptions _options;

    public PasswordService(IOptions<PasswordOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);
        var hash = ComputeHash(
            password,
            salt,
            _options.MemorySize,
            _options.Iterations,
            _options.Parallelism,
            _options.HashSize);
        return Encode(_options.MemorySize, _options.Iterations, _options.Parallelism, salt, hash);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (!TryDecode(hashedPassword, out var memorySize, out var iterations, out var parallelism, out var salt, out var storedHash))
            return false;

        var computed = ComputeHash(password, salt, memorySize, iterations, parallelism, storedHash.Length);
        return CryptographicOperations.FixedTimeEquals(computed, storedHash);
    }

    private static byte[] ComputeHash(
        string password,
        byte[] salt,
        int memorySize,
        int iterations,
        int parallelism,
        int hashSize)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = parallelism,
            MemorySize = memorySize,
            Iterations = iterations
        };
        return argon2.GetBytes(hashSize);
    }

    private static string Encode(int memorySize, int iterations, int parallelism, byte[] salt, byte[] hash)
    {
        return $"${AlgorithmId}$v={Version}$m={memorySize},t={iterations},p={parallelism}${ToBase64(salt)}${ToBase64(hash)}";
    }

    private static bool TryDecode(
        string encoded,
        out int memorySize,
        out int iterations,
        out int parallelism,
        out byte[] salt,
        out byte[] hash)
    {
        memorySize = 0;
        iterations = 0;
        parallelism = 0;
        salt = [];
        hash = [];

        if (string.IsNullOrEmpty(encoded))
            return false;

        var parts = encoded.Split('$');
        if (parts.Length != 6 || parts[1] != AlgorithmId || parts[2] != $"v={Version}")
            return false;

        var parameters = parts[3].Split(',');
        if (parameters.Length != 3
            || !parameters[0].StartsWith("m=")
            || !parameters[1].StartsWith("t=")
            || !parameters[2].StartsWith("p=")
            || !int.TryParse(parameters[0][2..], out memorySize)
            || !int.TryParse(parameters[1][2..], out iterations)
            || !int.TryParse(parameters[2][2..], out parallelism))
            return false;

        try
        {
            salt = FromBase64(parts[4]);
            hash = FromBase64(parts[5]);
        }
        catch (FormatException)
        {
            return false;
        }

        return salt.Length > 0 && hash.Length > 0 && memorySize > 0 && iterations > 0 && parallelism > 0;
    }

    private static string ToBase64(byte[] bytes) => Convert.ToBase64String(bytes).TrimEnd('=');

    private static byte[] FromBase64(string value)
    {
        var padded = value.Length % 4 == 0 ? value : value + new string('=', 4 - value.Length % 4);
        return Convert.FromBase64String(padded);
    }
}
