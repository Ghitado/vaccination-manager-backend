using VaccinationManager.Application.Dtos.VaccinationRecords;

namespace VaccinationManager.Application.Dtos.Persons;

public record PersonResponse(
	Guid Id,
	string Name,
	List<VaccinationRecordResponse> VaccinationRecords);

