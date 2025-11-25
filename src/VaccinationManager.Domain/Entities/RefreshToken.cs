using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class RefreshToken
{
	public Guid Id { get; private set; } 
	public Guid UserId { get; private set; }
	public string TokenHash { get; private set; } = string.Empty;
	public DateTime Expires { get; private set; }
	public bool IsRevoked { get; private set; } = false;

	public User? User { get; private set; }

	private RefreshToken() { }

	public RefreshToken(Guid userId, string tokenHash, DateTime expires)
	{
		var expiresUtc = expires.ToUniversalTime();

		if (userId == Guid.Empty)
			throw new DomainException("UserId is required.");

		if (string.IsNullOrWhiteSpace(tokenHash))
			throw new DomainException("Token hash is required.");

		if (expiresUtc <= DateTime.UtcNow)
			throw new DomainException("Expiration date must be in the future.");

		Id = Guid.NewGuid();
		UserId = userId;
		TokenHash = tokenHash;
		Expires = expiresUtc;
		IsRevoked = false;
	}

	public bool IsActive => !IsRevoked && Expires > DateTime.UtcNow;

	public void Revoke() => IsRevoked = true;
}
