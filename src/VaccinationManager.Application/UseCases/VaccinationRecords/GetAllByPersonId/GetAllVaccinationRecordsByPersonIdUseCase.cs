using Mapster;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetByPersonId;

public class GetAllVaccinationRecordsByPersonIdUseCase : IGetAllVaccinationRecordsByPersonIdUseCase
{
	private readonly IVaccinationRecordRepository _recordRepository;

	public GetAllVaccinationRecordsByPersonIdUseCase(IVaccinationRecordRepository recordRepository)
	{
		_recordRepository = recordRepository;
	}

	public async Task<PaginatedResult<VaccinationRecordResponse>> Execute(Guid personId, int? pageNumber, int? pageSize)
	{
		if (personId == Guid.Empty)
			throw new ArgumentException("Invalid person id");

		var records = await _recordRepository.GetAllByPersonId(personId, pageNumber, pageSize);

		return records.Adapt<PaginatedResult<VaccinationRecordResponse>>();
	}
}

