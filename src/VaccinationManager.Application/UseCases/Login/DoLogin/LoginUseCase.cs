using System.Security.Authentication;
using VaccinationManager.Application.Dtos.Auth;
using VaccinationManager.Application.Dtos.Login;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Domain.Security.Cryptography;
using VaccinationManager.Domain.Security.Tokens;

namespace VaccinationManager.Application.UseCases.Login.DoLogin;

public class LoginUseCase : ILoginUseCase
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly ITokenService _tokenService;
	private readonly IRefreshTokenRepository _refreshTokenRepository;

	public LoginUseCase(
		IUserRepository userRepository,
		IPasswordHasher passwordHasher,
		ITokenService tokenService,
		IRefreshTokenRepository refreshTokenRepository)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_tokenService = tokenService;
		_refreshTokenRepository = refreshTokenRepository;
	}

	public async Task<LoginResponse> Execute(LoginRequest request)
	{
		var user = await _userRepository.FindByEmailAsync(request.Email);

		if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
			throw new InvalidCredentialException("Invalid email or password.");

		var accessToken = _tokenService.CreateToken(user);
		var refreshTokenString = _tokenService.GenerateRefreshToken();

		var refreshTokenEntity = new RefreshToken(
			userId: user.Id,
			tokenHash: refreshTokenString,
			expires: DateTime.UtcNow.AddDays(7)
		);

		await _refreshTokenRepository.AddAsync(refreshTokenEntity);

		return new LoginResponse(accessToken, refreshTokenString);
	}
}

