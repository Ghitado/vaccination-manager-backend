using VaccinationManager.Application.Dtos.Users;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Domain.Security.Cryptography;

namespace VaccinationManager.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;

	public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
	}

	public async Task Execute(RegisterRequest request)
	{
		var emailExists = await _userRepository.FindByEmailAsync(request.Email);

		if (emailExists is not null)
			throw new ArgumentException("User with this email already exists.");

		var passwordHash = _passwordHasher.HashPassword(request.Password);

		var user = new User(request.Email, passwordHash);

		await _userRepository.AddAsync(user);
	}
}