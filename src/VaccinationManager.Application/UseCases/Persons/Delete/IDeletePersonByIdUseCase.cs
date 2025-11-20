namespace VaccinationManager.Application.UseCases.Persons.Delete;

public interface IDeletePersonByIdUseCase
{
	Task Execute(Guid recordId);
}
