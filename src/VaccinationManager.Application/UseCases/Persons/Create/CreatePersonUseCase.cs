using Mapster;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Persons.Create;

public class CreatePersonUseCase : ICreatePersonUseCase
{
	private readonly IPersonRepository _personRepository;

	public CreatePersonUseCase(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	public async Task<PersonResponse> Execute(CreatePersonRequest request)
	{
		if (string.IsNullOrWhiteSpace(request.Name))
			throw new ArgumentException("Name is required.");

		var person = new Person(request.Name);
		await _personRepository.Add(person);

		return person.Adapt<PersonResponse>();
	}
}

