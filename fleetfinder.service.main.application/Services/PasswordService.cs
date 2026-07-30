using System.Security.Cryptography;
using System.Text;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using Microsoft.Extensions.Options;

namespace fleetfinder.service.main.application.Services;

public class PasswordService : IPasswordService
{
    private readonly byte[] _key;
    private const int AesBlockSize = 16;
    private const int AesKeySize = 32;

    public PasswordService(IOptions<PasswordOptions> options)
    {
        var passwordOptions = options.Value ?? throw new ArgumentNullException(nameof(options));
        if (string.IsNullOrWhiteSpace(passwordOptions.EncryptionKey))
        {
            throw new InvalidOperationException("PasswordOptions.EncryptionKey not set");
        }

        _key = Convert.FromBase64String(passwordOptions.EncryptionKey);
        if (_key.Length != AesKeySize)
        {
            throw new InvalidOperationException($"EncryptionKey must be {AesKeySize} bytes in Base64");
        }
    }

    public string EncryptPassword(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return plainText;
        }

        byte[] iv = RandomNumberGenerator.GetBytes(AesBlockSize);
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;
        using ICryptoTransform encryptor = aes.CreateEncryptor();
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        byte[] result = new byte[iv.Length + cipherBytes.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, iv.Length, cipherBytes.Length);
        return Convert.ToBase64String(result);
    }

    public bool VerifyPassword(string password, string encryptedPassword)
    {
        try
        {
            string decrypted = DecryptPassword(encryptedPassword);
            return decrypted == password;
        }
        catch
        {
            return false;
        }
    }

    public string DecryptPassword(string encryptedPassword)
    {
        if (string.IsNullOrEmpty(encryptedPassword))
        {
            return encryptedPassword;
        }

        byte[] full = Convert.FromBase64String(encryptedPassword);
        if (full.Length < AesBlockSize)
        {
            throw new CryptographicException("Invalid encrypted data");
        }

        byte[] iv = new byte[AesBlockSize];
        byte[] cipherBytes = new byte[full.Length - AesBlockSize];
        Buffer.BlockCopy(full, 0, iv, 0, AesBlockSize);
        Buffer.BlockCopy(full, AesBlockSize, cipherBytes, 0, cipherBytes.Length);
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;
        using ICryptoTransform decryptor = aes.CreateDecryptor();
        byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
}
