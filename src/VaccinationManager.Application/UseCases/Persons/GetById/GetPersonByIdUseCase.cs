using Mapster;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Persons.GetById;

public class GetPersonByIdUseCase : IGetPersonByIdUseCase
{
	private readonly IPersonRepository _repository;

	public GetPersonByIdUseCase(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<PersonResponse> Execute(Guid id)
	{
		if (id == Guid.Empty)
			throw new ArgumentException("Invalid id");

		var person = await _repository.GetById(id);

		if (person is null)
			throw new ArgumentException("Person not found");

		return person.Adapt<PersonResponse>();
	}
}

