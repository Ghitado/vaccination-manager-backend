using VaccinationManager.Application.Dtos.VaccinationRecords;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetById;

public interface IGetVaccinationRecordByIdUseCase
{
	Task<VaccinationRecordResponse> Execute(Guid recordId);
}
