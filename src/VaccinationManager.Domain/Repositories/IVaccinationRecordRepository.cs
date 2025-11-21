using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Repositories;

public interface IVaccinationRecordRepository
{
	Task<VaccinationRecord> Add(VaccinationRecord record);
	Task<VaccinationRecord?> GetById(Guid id);
	Task Delete(VaccinationRecord record);
}
