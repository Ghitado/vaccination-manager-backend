using Mapster;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.Vaccines.GetAll;

public class GetAllVaccinesUseCase : IGetAllVaccinesUseCase
{
	private readonly IVaccineRepository _vaccineRepository;

	public GetAllVaccinesUseCase(IVaccineRepository vaccineRepository)
	{
		_vaccineRepository = vaccineRepository;	
	}

	public async Task<PaginatedResult<VaccineResponse>> Execute(int? pageNumber, int? pageSize)
	{
		if (pageNumber is < 1)
			throw new ArgumentException("pageNumber must be >= 1", nameof(pageNumber));

		if (pageSize is <= 0)
			throw new ArgumentException("pageSize must be > 0", nameof(pageSize));

		var vaccines = await _vaccineRepository.GetAll(pageNumber, pageSize);

		return vaccines.Adapt<PaginatedResult<VaccineResponse>>();
	}
}

