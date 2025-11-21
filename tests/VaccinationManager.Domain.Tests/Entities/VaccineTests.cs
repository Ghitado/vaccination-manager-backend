using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class VaccineTests
{
	[Fact]
	public void Constructor_WithValidName_ShouldCreateVaccine()
	{
		var vaccine = new Vaccine("COVID-19");

		vaccine.Id.Should().NotBeEmpty();
		vaccine.Name.Should().Be("COVID-19");
	}

	[Fact]
	public void Constructor_WithEmptyName_ShouldThrowDomainException()
	{
		Action act = () => new Vaccine("");

		act.Should().Throw<DomainException>();
	}
}
