namespace FleetFinder.Application.Abstractions.Identity;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
