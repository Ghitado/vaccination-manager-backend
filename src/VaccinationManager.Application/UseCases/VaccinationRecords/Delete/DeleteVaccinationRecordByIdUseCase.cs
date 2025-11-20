using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.Delete;

public class DeleteVaccinationRecordByIdUseCase : IDeleteVaccinationRecordByIdUseCase
{
	private readonly IVaccinationRecordRepository _recordRepository;

	public DeleteVaccinationRecordByIdUseCase(IVaccinationRecordRepository recordRepository)
	{
		_recordRepository = recordRepository;
	}

	public async Task Execute(Guid recordId)
	{
		if (recordId == Guid.Empty)
			throw new ArgumentException("Invalid vaccination record id");

		var record = await _recordRepository.GetById(recordId);

		if (record is null)
			throw new ArgumentException("Vaccination record not found");

		await _recordRepository.Delete(record);
	}
}

