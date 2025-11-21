using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Application.UseCases.Vaccines.GetPaginated;

public interface IGetPaginatedVaccinesUseCase
{
	Task<PaginatedResult<VaccineResponse>> Execute(int pageNumber, int pageSize);
}
