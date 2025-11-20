using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetAll;

public interface IGetAllVaccinationRecordsUseCase
{
	Task<PaginatedResult<VaccinationRecordResponse>> Execute(int? pageNumber, int? pageSize);
}
