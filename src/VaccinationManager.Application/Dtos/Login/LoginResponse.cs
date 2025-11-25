namespace VaccinationManager.Application.Dtos.Auth;

public record LoginResponse(
	string AccessToken, 
	string RefreshToken);
