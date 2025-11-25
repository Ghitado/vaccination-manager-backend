namespace VaccinationManager.Domain.Security.Cryptography;

public interface IPasswordHasher
{
	string HashPassword(string password);
	bool VerifyPassword(string password, string passwordHash);
}
