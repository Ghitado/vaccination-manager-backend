using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IVaccinationRecordRepository
{
	Task<PaginatedResult<VaccinationRecord>> GetAll(int? pageNumber, int? pageSize);
	Task<VaccinationRecord?> GetById(Guid id);
	Task<PaginatedResult<VaccinationRecord>> GetAllByPersonId(Guid personId, int? pageNumber, int? pageSize);
	Task<VaccinationRecord> Add(VaccinationRecord record);
	Task Delete(VaccinationRecord record);
}
