using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IUserRepository
{
	Task<User?> FindByEmailAsync(string email);
	Task AddAsync(User user);
}
