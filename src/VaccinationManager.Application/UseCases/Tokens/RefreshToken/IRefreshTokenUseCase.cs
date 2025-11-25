using VaccinationManager.Application.Dtos.Auth;

namespace VaccinationManager.Application.UseCases.Tokens.RefreshToken;

public interface IRefreshTokenUseCase
{
	Task<LoginResponse> Execute(string refreshToken);
}
