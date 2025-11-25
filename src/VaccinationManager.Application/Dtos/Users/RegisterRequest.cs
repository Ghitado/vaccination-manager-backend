using System.ComponentModel.DataAnnotations;

namespace VaccinationManager.Application.Dtos.Users;

public record RegisterRequest(
	[Required(ErrorMessage = "Email is required.")]
	[EmailAddress(ErrorMessage = "Invalid email format.")]
	string Email,
	[Required(ErrorMessage = "Password is required.")]
	[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
	[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$",
		ErrorMessage = "Password must contain: 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]
	string Password);

