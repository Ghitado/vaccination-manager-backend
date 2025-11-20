using VaccinationManager.Application.Dtos.Persons;

namespace VaccinationManager.Application.UseCases.Persons.Create;

public interface ICreatePersonUseCase
{
	Task<PersonResponse> Execute(CreatePersonRequest request);
}
