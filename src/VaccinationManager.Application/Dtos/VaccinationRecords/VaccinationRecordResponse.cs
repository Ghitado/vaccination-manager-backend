namespace VaccinationManager.Application.Dtos.VaccinationRecords;

public record VaccinationRecordResponse(
	Guid Id, 
	Guid PersonId, 
	Guid VaccineId, 
	DateTime Date, 
	int Dose);

