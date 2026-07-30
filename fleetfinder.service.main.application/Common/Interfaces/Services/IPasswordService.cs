namespace fleetfinder.service.main.application.Common.Interfaces.Services;

public interface IPasswordService
{
    string EncryptPassword(string plainText);
    string DecryptPassword(string encryptedPassword);
    bool VerifyPassword(string password, string encryptedPassword);
}
