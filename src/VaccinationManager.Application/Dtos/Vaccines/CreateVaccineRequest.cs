using System.ComponentModel.DataAnnotations;

namespace VaccinationManager.Application.Dtos.Vaccines;

public record CreateVaccineRequest(
	[Required(ErrorMessage = "Vaccine name is required.")]
	[StringLength(100, ErrorMessage = "Vaccine name cannot exceed 100 characters.")]
	string Name);

