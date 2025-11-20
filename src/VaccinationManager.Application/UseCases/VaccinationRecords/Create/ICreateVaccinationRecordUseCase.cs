using VaccinationManager.Application.Dtos.VaccinationRecords;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.Create;

public interface ICreateVaccinationRecordUseCase
{
	Task<VaccinationRecordResponse> Execute(CreateVaccinationRecordRequest request);
}
