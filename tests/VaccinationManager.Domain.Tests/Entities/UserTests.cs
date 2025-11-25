using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class UserTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateUser_AndLowerCaseEmail()
	{
		var emailInput = "Test@Example.COM";
		var passwordHash = "hashed_password_123";

		var user = new User(emailInput, passwordHash);

		user.Id.Should().NotBeEmpty();
		user.Email.Should().Be("test@example.com"); 
		user.PasswordHash.Should().Be(passwordHash);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Constructor_WithInvalidEmail_ShouldThrowDomainException(string invalidEmail)
	{
		var passwordHash = "valid_hash";

		Action act = () => new User(invalidEmail, passwordHash);

		act.Should().Throw<DomainException>();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Constructor_WithInvalidPasswordHash_ShouldThrowDomainException(string invalidHash)
	{
		var email = "valid@example.com";

		Action act = () => new User(email, invalidHash);

		act.Should().Throw<DomainException>();
	}
}

