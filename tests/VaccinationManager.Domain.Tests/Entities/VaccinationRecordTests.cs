using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class VaccinationRecordTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateVaccinationRecord()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();
		var appliedAt = DateTime.UtcNow;
		var dose = 1;

		var record = new VaccinationRecord(personId, vaccineId, appliedAt, dose);

		record.Id.Should().NotBeEmpty();
		record.PersonId.Should().Be(personId);
		record.VaccineId.Should().Be(vaccineId);
		record.AppliedAt.Should().Be(appliedAt);
		record.Dose.Should().Be(dose);
	}

	[Fact]
	public void Constructor_WithEmptyPersonId_ShouldThrowDomainException()
	{
		var vaccineId = Guid.NewGuid();
		var appliedAt = DateTime.UtcNow;
		Action act = () => new VaccinationRecord(Guid.Empty, vaccineId, appliedAt, 1);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithEmptyVaccineId_ShouldThrowDomainException()
	{
		var personId = Guid.NewGuid();
		var appliedAt = DateTime.UtcNow;
		Action act = () => new VaccinationRecord(personId, Guid.Empty, appliedAt, 1);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithDefaultDate_ShouldThrowDomainException()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();
		Action act = () => new VaccinationRecord(personId, vaccineId, default, 1);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithFutureDate_ShouldThrowDomainException()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();
		var futureDate = DateTime.UtcNow.AddDays(1); 

		Action act = () => new VaccinationRecord(personId, vaccineId, futureDate, 1);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void Constructor_WithInvalidDose_ShouldThrowDomainException()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();
		var appliedAt = DateTime.UtcNow;
		Action act = () => new VaccinationRecord(personId, vaccineId, appliedAt, 0);

		act.Should().Throw<DomainException>();
	}
}