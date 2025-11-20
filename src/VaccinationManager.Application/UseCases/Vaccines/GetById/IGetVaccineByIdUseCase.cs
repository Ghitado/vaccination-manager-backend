using VaccinationManager.Application.Dtos.Vaccines;

namespace VaccinationManager.Application.UseCases.Vaccines.GetById;

public interface IGetVaccineByIdUseCase
{
	Task<VaccineResponse?> Execute(Guid id);
}
