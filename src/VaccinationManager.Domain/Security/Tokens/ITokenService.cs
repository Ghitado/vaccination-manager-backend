using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Security.Tokens;

public interface ITokenService
{
	string CreateToken(User user);
	string GenerateRefreshToken();
	string HashToken(string token);
}
