using VaccinationManager.Application.Dtos.Persons;

namespace VaccinationManager.Application.UseCases.Persons.GetById;

public interface IGetPersonByIdUseCase
{
	Task<PersonResponse> Execute(Guid id);
}
