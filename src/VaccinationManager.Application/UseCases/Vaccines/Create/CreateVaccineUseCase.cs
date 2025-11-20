using Mapster;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Vaccines.Create;

public class CreateVaccineUseCase : ICreateVaccineUseCase
{
	private readonly IVaccineRepository _vaccineRepository;

	public CreateVaccineUseCase(IVaccineRepository vaccineRepository)
	{
		_vaccineRepository = vaccineRepository;
	}

	public async Task<VaccineResponse> Execute(CreateVaccineRequest request)
	{
		if (string.IsNullOrWhiteSpace(request.Name))
			throw new ArgumentException("Name is required.");

		var vaccine = new Vaccine(request.Name);
		await _vaccineRepository.Add(vaccine);

		return vaccine.Adapt<VaccineResponse>();
	}
}

