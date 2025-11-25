using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class User
{
	public Guid Id { get; private set; }
	public string Email { get; private set; } = string.Empty;
	public string PasswordHash { get; private set; } = string.Empty;
	private User() { }

	public User(string email, string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new DomainException("Email is required.");
		if (string.IsNullOrWhiteSpace(passwordHash))
			throw new DomainException("Password hash is required.");

		Id = Guid.NewGuid();
		Email = email.ToLower();
		PasswordHash = passwordHash;
	}
}

