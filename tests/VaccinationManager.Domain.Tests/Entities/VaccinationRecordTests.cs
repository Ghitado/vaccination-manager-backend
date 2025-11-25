using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class VaccinationRecordTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateVaccinationRecord_AndConvertToUtc()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();
		var appliedAtLocal = DateTime.Now; 
		var dose = 1;

		var record = new VaccinationRecord(personId, vaccineId, appliedAtLocal, dose);

		record.Id.Should().NotBeEmpty();
		record.PersonId.Should().Be(personId);
		record.VaccineId.Should().Be(vaccineId);

		record.AppliedAt.Should().Be(appliedAtLocal.ToUniversalTime());
		record.AppliedAt.Kind.Should().Be(DateTimeKind.Utc);

		record.Dose.Should().Be(dose);
	}

	[Fact]
	public void Constructor_WithEmptyPersonId_ShouldThrowDomainException()
	{
		Action act = () => new VaccinationRecord(Guid.Empty, Guid.NewGuid(), DateTime.UtcNow, 1);
		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithEmptyVaccineId_ShouldThrowDomainException()
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.Empty, DateTime.UtcNow, 1);
		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithDefaultDate_ShouldThrowDomainException()
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), default, 1);
		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithFutureDate_ShouldThrowDomainException()
	{
		var futureDate = DateTime.UtcNow.AddDays(1);
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), futureDate, 1);

		act.Should().Throw<DomainException>();
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-100)]
	public void Constructor_WithDoseZeroOrNegative_ShouldThrowDomainException(int invalidDose)
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, invalidDose);

		act.Should().Throw<DomainException>();
	}

	[Theory]
	[InlineData(51)]
	[InlineData(100)]
	public void Constructor_WithDoseGreaterThan50_ShouldThrowDomainException(int invalidDose)
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, invalidDose);

		act.Should().Throw<DomainException>();
	}

	[Theory]
	[InlineData(1)]  
	[InlineData(50)] 
	[InlineData(25)] 
	public void Constructor_WithBoundaryDoses_ShouldCreateSuccessfully(int validDose)
	{
		var record = new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, validDose);

		record.Dose.Should().Be(validDose);
	}
}