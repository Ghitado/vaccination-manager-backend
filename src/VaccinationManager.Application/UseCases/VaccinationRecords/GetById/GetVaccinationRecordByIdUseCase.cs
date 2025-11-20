using Mapster;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.GetById;

public class GetVaccinationRecordByIdUseCase : IGetVaccinationRecordByIdUseCase
{
	private readonly IVaccinationRecordRepository _recordRepository;

	public GetVaccinationRecordByIdUseCase(IVaccinationRecordRepository recordRepository)
	{
		_recordRepository = recordRepository;
	}

	public async Task<VaccinationRecordResponse> Execute(Guid recordId)
	{
		if (recordId == Guid.Empty)
			throw new ArgumentException("Invalid vaccination record id");

		var record = await _recordRepository.GetById(recordId);

		if (record is null)
			throw new ArgumentException("Vaccination record not found");

		return record.Adapt<VaccinationRecordResponse>();
	}
}

