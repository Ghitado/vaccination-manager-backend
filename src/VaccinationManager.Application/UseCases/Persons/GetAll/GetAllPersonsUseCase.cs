using Mapster;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Persons.GetAll;

public class GetAllPersonsUseCase : IGetAllPersonsUseCase
{
	private readonly IPersonRepository _repository;

	public GetAllPersonsUseCase(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<PaginatedResult<PersonResponse>> Execute(int? pageNumber, int? pageSize)
	{
		if (pageNumber is < 1)
			throw new ArgumentException("pageNumber must be >= 1", nameof(pageNumber));

		if (pageSize is <= 0)
			throw new ArgumentException("pageSize must be > 0", nameof(pageSize));

		var persons = await _repository.GetAll(pageNumber, pageSize);

		return persons.Adapt<PaginatedResult<PersonResponse>>();
	}
}

