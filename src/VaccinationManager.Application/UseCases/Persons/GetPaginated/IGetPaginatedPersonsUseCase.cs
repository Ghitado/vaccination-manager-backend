using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.Persons.GetPaginated;

public interface IGetPaginatedPersonsUseCase
{
	Task<PaginatedResult<PaginatedPersonResponse>> Execute(int pageNumber, int pageSize);
}
