using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Persons.Delete;

public class DeletePersonByIdUseCase : IDeletePersonByIdUseCase
{
	private readonly IPersonRepository _personRepository;

	public DeletePersonByIdUseCase(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	public async Task Execute(Guid recordId)
	{
		if (recordId == Guid.Empty)
			throw new ArgumentException("Invalid vaccination record id");

		var person = await _personRepository.GetById(recordId);

		if (person is null)
			throw new ArgumentException("Person not found");

		await _personRepository.Delete(person);
	}
}

