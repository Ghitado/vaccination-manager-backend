using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class VaccineTests
{
	[Fact]
	public void Constructor_WithValidName_CreatesVaccine()
	{
		var vaccine = new Vaccine("COVID");

		vaccine.Name.Should().Be("COVID");
		vaccine.Id.Should().NotBeEmpty();
	}

	[Fact]
	public void Constructor_WithEmptyName_ThrowsException()
	{
		Action act = () => new Vaccine("");

		act.Should().Throw<DomainException>()
			.WithMessage("Name is required.");
	}

	[Fact]
	public void UpdateName_WithValidName_Updates()
	{
		var vaccine = new Vaccine("Old");

		vaccine.UpdateName("New");

		vaccine.Name.Should().Be("New");
	}

	[Fact]
	public void UpdateName_WithEmptyName_ThrowsException()
	{
		var vaccine = new Vaccine("Valid");

		Action act = () => vaccine.UpdateName("");

		act.Should().Throw<DomainException>()
			.WithMessage("Name is required.");
	}
}
