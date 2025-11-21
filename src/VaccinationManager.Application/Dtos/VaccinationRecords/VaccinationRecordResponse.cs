namespace VaccinationManager.Application.Dtos.VaccinationRecords;

public record VaccinationRecordResponse(
	Guid Id,
	Guid VaccineId,
	string VaccineName,
	DateTime AppliedAt,
	int Dose);

