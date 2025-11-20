using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetByPersonId;

public interface IGetAllVaccinationRecordsByPersonIdUseCase
{
	Task<PaginatedResult<VaccinationRecordResponse>> Execute(Guid personId, int? pageNumber, int? pageSize);
}
