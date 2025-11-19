using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class VaccinationRecordTests
{
	[Fact]
	public void Constructor_WithValidData_CreatesRecord()
	{
		var personId = Guid.NewGuid();
		var vaccineId = Guid.NewGuid();

		var record = new VaccinationRecord(personId, vaccineId, DateTime.Today, 1);

		record.PersonId.Should().Be(personId);
		record.VaccineId.Should().Be(vaccineId);
		record.Dose.Should().Be(1);
	}

	[Fact]
	public void Constructor_WithEmptyPersonId_Throws()
	{
		Action act = () => new VaccinationRecord(Guid.Empty, Guid.NewGuid(), DateTime.Today, 1);

		act.Should().Throw<DomainException>()
			.WithMessage("PersonId is required.");
	}

	[Fact]
	public void Constructor_WithEmptyVaccineId_Throws()
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.Empty, DateTime.Today, 1);

		act.Should().Throw<DomainException>()
			.WithMessage("VaccineId is required.");
	}

	[Fact]
	public void Constructor_WithInvalidDate_Throws()
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), default, 1);

		act.Should().Throw<DomainException>()
			.WithMessage("Invalid date.");
	}

	[Fact]
	public void Constructor_WithInvalidDose_Throws()
	{
		Action act = () => new VaccinationRecord(Guid.NewGuid(), Guid.NewGuid(), DateTime.Today, 0);

		act.Should().Throw<DomainException>()
			.WithMessage("Invalid dose.");
	}
}
