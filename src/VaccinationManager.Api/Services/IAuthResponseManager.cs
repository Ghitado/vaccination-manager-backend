using VaccinationManager.Application.Dtos.Auth;

namespace VaccinationManager.Api.Services;

public interface IAuthResponseManager
{
	void SetTokensAndPrepareResponse(LoginResponse response, HttpContext context);
}