using Mapster;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetAll;

public class GetAllVaccinationRecordsUseCase : IGetAllVaccinationRecordsUseCase
{
	private readonly IVaccinationRecordRepository _recordRepository;

	public GetAllVaccinationRecordsUseCase(IVaccinationRecordRepository recordRepository)
	{
		_recordRepository = recordRepository;
	}

	public async Task<PaginatedResult<VaccinationRecordResponse>> Execute(int? pageNumber, int? pageSize)
	{
		if (pageNumber is < 1)
			throw new ArgumentException("pageNumber must be >= 1", nameof(pageNumber));

		if (pageSize is <= 0)
			throw new ArgumentException("pageSize must be > 0", nameof(pageSize));

		var records = await _recordRepository.GetAll(pageNumber, pageSize);

		return records.Adapt<PaginatedResult<VaccinationRecordResponse>>();
	}
}

