using System.ComponentModel.DataAnnotations;

namespace VaccinationManager.Application.Dtos.Persons;

public record CreatePersonRequest(
	[Required(ErrorMessage = "Name is required.")]
	[StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
	string Name);

