using VaccinationManager.Application.Dtos.Auth;
using VaccinationManager.Application.Dtos.Login;

namespace VaccinationManager.Application.UseCases.Login.DoLogin;

public interface ILoginUseCase
{
	Task<LoginResponse> Execute(LoginRequest request);
}
