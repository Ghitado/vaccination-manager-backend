using VaccinationManager.Application.Dtos.Vaccines;

namespace VaccinationManager.Application.UseCases.Vaccines.Create;

public interface ICreateVaccineUseCase
{
	Task<VaccineResponse> Execute(CreateVaccineRequest request);
}
