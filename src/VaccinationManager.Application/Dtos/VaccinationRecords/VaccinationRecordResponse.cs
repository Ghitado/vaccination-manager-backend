using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Application.Dtos.Vaccines;

namespace VaccinationManager.Application.Dtos.VaccinationRecords;

public record VaccinationRecordResponse(
	Guid Id,
	PersonResponse Person,
	VaccineResponse Vaccine,
	DateTime Date,
	int Dose);

