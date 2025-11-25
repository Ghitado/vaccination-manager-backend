using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
	private readonly VaccinationManagerDbContext _dbContext;

	public RefreshTokenRepository(VaccinationManagerDbContext dbContext) => _dbContext = dbContext;

	public async Task<RefreshToken?> FindByTokenHashAsync(string tokenHash)
	{
		return await _dbContext.RefreshTokens
			.AsNoTracking()
			.Include(token => token.User)
			.FirstOrDefaultAsync(token => token.TokenHash.Equals(tokenHash) && token.IsRevoked == false);
	}

	public async Task AddAsync(RefreshToken refreshToken)
	{
		await _dbContext.RefreshTokens
			.Where(token => token.UserId == refreshToken.UserId && token.IsRevoked == false)
			.ExecuteDeleteAsync();

		await _dbContext.RefreshTokens.AddAsync(refreshToken);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(RefreshToken token)
	{
		_dbContext.RefreshTokens.Update(token);
		await _dbContext.SaveChangesAsync();
	}
}

