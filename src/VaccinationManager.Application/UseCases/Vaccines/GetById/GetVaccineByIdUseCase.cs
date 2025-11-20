using Mapster;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Vaccines.GetById;

public class GetVaccineByIdUseCase : IGetVaccineByIdUseCase
{
	private readonly IVaccineRepository _vaccineRepository;

	public GetVaccineByIdUseCase(IVaccineRepository vaccineRepository)
	{
		_vaccineRepository = vaccineRepository;
	}

	public async Task<VaccineResponse?> Execute(Guid id)
	{
		if (id == Guid.Empty)
			throw new ArgumentException("Invalid id");

		var vaccine = await _vaccineRepository.GetById(id);

		if (vaccine is null)
			throw new ArgumentException("Vaccine not found");

		return vaccine.Adapt<VaccineResponse>();
	}
}

