using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
	private readonly VaccinationManagerDbContext _dbContext;

	public UserRepository(VaccinationManagerDbContext dbContext) => _dbContext = dbContext;

	public async Task<User?> FindByEmailAsync(string email)
	{
		return await _dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(u => u.Email == email);
	}

	public async Task AddAsync(User user)
	{
		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();
	}
}

