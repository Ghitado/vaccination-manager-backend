using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IPersonRepository
{
	Task<Person?> GetById(Guid id);
	Task<PaginatedResult<Person>> GetPaginated(int pageNumber = 1, int pageSize = 10);
	Task<Person> Add(Person person);
	Task Delete(Person person);
}