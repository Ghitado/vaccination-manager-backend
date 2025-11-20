using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.Vaccines.GetAll;

public interface IGetAllVaccinesUseCase
{
	Task<PaginatedResult<VaccineResponse>> Execute(int? pageNumber, int? pageSize);
}
