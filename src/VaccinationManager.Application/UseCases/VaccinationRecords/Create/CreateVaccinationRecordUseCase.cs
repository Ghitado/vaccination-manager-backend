using Mapster;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Application.UseCases.VaccinationRecords.Create;

public class CreateVaccinationRecordUseCase : ICreateVaccinationRecordUseCase
{
	private readonly IVaccinationRecordRepository _recordRepository;
	private readonly IPersonRepository _personRepository;
	private readonly IVaccineRepository _vaccineRepository;

	public CreateVaccinationRecordUseCase(
		IVaccinationRecordRepository recordRepository,
		IPersonRepository personRepository,
		IVaccineRepository vaccineRepository)
	{
		_recordRepository = recordRepository;
		_personRepository = personRepository;
		_vaccineRepository = vaccineRepository;
	}

	public async Task<VaccinationRecordResponse> Execute(CreateVaccinationRecordRequest request)
	{
		var person = await _personRepository.GetById(request.PersonId)
			?? throw new ArgumentException("Person not found");

		var vaccine = await _vaccineRepository.GetById(request.VaccineId)
			?? throw new ArgumentException("Vaccine not found");

		if (request.Dose is <= 0)
			throw new ArgumentException("Dose must be greater than 0");

		var record = new VaccinationRecord(person.Id, vaccine.Id, request.AppliedAt, request.Dose);
		await _recordRepository.Add(record);

		return record.Adapt<VaccinationRecordResponse>();
	}
}

