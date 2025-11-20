namespace VaccinationManager.Application.UseCases.VaccinationRecords.Delete;

public interface IDeleteVaccinationRecordByIdUseCase
{
	Task Execute(Guid recordId);
}
