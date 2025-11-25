using System.Security.Authentication;
using VaccinationManager.Application.Dtos.Auth;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Domain.Security.Tokens;

namespace VaccinationManager.Application.UseCases.Tokens.RefreshToken;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
	private readonly IRefreshTokenRepository _refreshTokenRepository;
	private readonly ITokenService _tokenService;

	public RefreshTokenUseCase(
		IRefreshTokenRepository refreshTokenRepository,
		ITokenService tokenService)
	{
		_refreshTokenRepository = refreshTokenRepository;
		_tokenService = tokenService;
	}

	public async Task<LoginResponse> Execute(string refreshTokenString)
	{
		var oldRefreshToken = await _refreshTokenRepository.FindByTokenHashAsync(refreshTokenString);

		if (oldRefreshToken is null)
			throw new InvalidCredentialException("Refresh token is invalid or does not exist."); // Usamos InvalidCredentialException para o 401

		if (oldRefreshToken.IsRevoked)
			throw new InvalidCredentialException("Refresh token has been revoked.");

		if (!oldRefreshToken.IsActive)
			throw new InvalidCredentialException("Refresh token has expired.");

		if (oldRefreshToken.User is null)
			throw new InvalidOperationException("User data missing for token validation.");

		oldRefreshToken.Revoke();
		await _refreshTokenRepository.UpdateAsync(oldRefreshToken);

		var newAccessToken = _tokenService.CreateToken(oldRefreshToken.User);
		var newRefreshTokenString = _tokenService.GenerateRefreshToken();

		var newRefreshTokenEntity = new Domain.Entities.RefreshToken(
			userId: oldRefreshToken.UserId,
			tokenHash: newRefreshTokenString, 
			expires: DateTime.UtcNow.AddDays(7)
		);

		await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

		return new LoginResponse(newAccessToken, newRefreshTokenString);
	}
}

