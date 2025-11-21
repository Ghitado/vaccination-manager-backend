namespace VaccinationManager.Application.Dtos.VaccinationRecords;

public record CreateVaccinationRecordRequest(
	Guid PersonId, 
	Guid VaccineId, 
	DateTime AppliedAt, 
	int Dose);

