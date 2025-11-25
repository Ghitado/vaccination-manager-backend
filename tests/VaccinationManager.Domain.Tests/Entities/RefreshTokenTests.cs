using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class RefreshTokenTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateRefreshToken()
	{
		var userId = Guid.NewGuid();
		var tokenHash = "valid_hash_string";
		var expires = DateTime.UtcNow.AddDays(7);

		var refreshToken = new RefreshToken(userId, tokenHash, expires);

		refreshToken.Id.Should().NotBeEmpty();
		refreshToken.UserId.Should().Be(userId);
		refreshToken.TokenHash.Should().Be(tokenHash);
		refreshToken.Expires.Should().Be(expires); 
		refreshToken.IsRevoked.Should().BeFalse();
		refreshToken.IsActive.Should().BeTrue();
	}

	[Fact]
	public void Constructor_WithEmptyUserId_ShouldThrowDomainException()
	{
		Action act = () => new RefreshToken(Guid.Empty, "hash", DateTime.UtcNow.AddDays(1));

		act.Should().Throw<DomainException>();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Constructor_WithInvalidTokenHash_ShouldThrowDomainException(string invalidHash)
	{
		Action act = () => new RefreshToken(Guid.NewGuid(), invalidHash, DateTime.UtcNow.AddDays(1));

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithPastExpiration_ShouldThrowDomainException()
	{
		var pastDate = DateTime.UtcNow.AddMinutes(-1);

		Action act = () => new RefreshToken(Guid.NewGuid(), "hash", pastDate);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Revoke_ShouldMarkTokenAsRevoked_AndInactive()
	{
		var token = new RefreshToken(Guid.NewGuid(), "hash", DateTime.UtcNow.AddDays(1));

		token.Revoke();

		token.IsRevoked.Should().BeTrue();
		token.IsActive.Should().BeFalse();
	}
}
