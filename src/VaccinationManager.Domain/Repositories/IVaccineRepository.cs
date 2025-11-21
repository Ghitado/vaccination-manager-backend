using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IVaccineRepository
{
	Task<PaginatedResult<Vaccine>> GetPaginated(int pageNumber = 1, int pageSize = 10);
	Task<Vaccine?> GetById(Guid id);
	Task<Vaccine> Add(Vaccine vaccine);
}