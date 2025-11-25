using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IRefreshTokenRepository
{
	Task<RefreshToken?> FindByTokenHashAsync(string tokenHash);
	Task AddAsync(RefreshToken token);
	Task UpdateAsync(RefreshToken token);
}
