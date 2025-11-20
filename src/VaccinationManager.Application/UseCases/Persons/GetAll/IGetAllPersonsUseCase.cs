using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.Persons.GetAll;

public interface IGetAllPersonsUseCase
{
	Task<PaginatedResult<PersonResponse>> Execute(int? pageNumber, int? pageSize);
}
