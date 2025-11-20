using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IPersonRepository
{
	Task<Person?> GetById(Guid id);
	Task<PaginatedResult<Person>> GetAll(int? pageNumber, int? pageSize);
	Task<Person> Add(Person person);
	Task Delete(Person person);
}