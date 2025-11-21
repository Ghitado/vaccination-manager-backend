using Mapster;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Persons.GetPaginated;

public class GetPaginatedPersonsUseCase : IGetPaginatedPersonsUseCase
{
	private readonly IPersonRepository _repository;

	public GetPaginatedPersonsUseCase(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<PaginatedResult<PaginatedPersonResponse>> Execute(int pageNumber, int pageSize)
	{
		if (pageNumber is < 1)
			throw new ArgumentException("pageNumber must be >= 1", nameof(pageNumber));

		if (pageSize is <= 0)
			throw new ArgumentException("pageSize must be > 0", nameof(pageSize));

		var persons = await _repository.GetPaginated(pageNumber, pageSize);

		return persons.Adapt<PaginatedResult<PaginatedPersonResponse>>();
	}
}

