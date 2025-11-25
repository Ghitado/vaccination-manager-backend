using System.ComponentModel.DataAnnotations;
using VaccinationManager.Application.Attributes;

namespace VaccinationManager.Application.Dtos.VaccinationRecords;

public record CreateVaccinationRecordRequest(
	[Required(ErrorMessage = "Person ID is required.")]
	Guid PersonId,
	[Required(ErrorMessage = "Vaccine ID is required.")]
	Guid VaccineId,
	[Required(ErrorMessage = "Application date is required.")]
	[NotFuture]
	DateTime AppliedAt,
	[Required(ErrorMessage = "Dose number is required.")]
	[Range(1, 50, ErrorMessage = "Dose must be greater than 0.")]
	int Dose);

