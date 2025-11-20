using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IVaccineRepository
{
	Task<PaginatedResult<Vaccine>> GetAll(int? pageNumber, int? pageSize);
	Task<Vaccine?> GetById(Guid id);
	Task<Vaccine> Add(Vaccine vaccine);
}