using FluentAssertions;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Domain.Tests.Entities;

public class PersonTests
{
	[Fact]
	public void Constructor_WithValidName_ShouldCreatePerson()
	{
		var person = new Person("John");

		person.Name.Should().Be("John");
		person.Id.Should().NotBeEmpty();
		person.VaccinationRecords.Should().BeEmpty();
	}

	[Fact]
	public void Constructor_WithEmptyName_ShouldThrowDomainException()
	{
		Action act = () => new Person("");

		act.Should().Throw<DomainException>()
			.WithMessage("Name is required.");
	}

	[Fact]
	public void AddVaccination_WithValidRecord_ShouldAddRecord()
	{
		var person = new Person("Maria");
		var vaccineId = Guid.NewGuid();
		var record = new VaccinationRecord(person.Id, vaccineId, DateTime.UtcNow, 1);

		person.AddVaccination(record);

		person.VaccinationRecords.Should().ContainSingle()
			.Which.Dose.Should().Be(1);
	}

	[Fact]
	public void AddVaccination_WithNullRecord_ShouldThrowDomainException()
	{
		var person = new Person("Maria");

		Action act = () => person.AddVaccination(null!);

		act.Should().Throw<DomainException>();
	}

	[Fact]
	public void AddVaccination_WithDuplicateDose_ShouldThrowDomainException()
	{
		var person = new Person("Carlos");
		var vaccineId = Guid.NewGuid();
		var record1 = new VaccinationRecord(person.Id, vaccineId, DateTime.UtcNow, 1);
		person.AddVaccination(record1);

		var record2 = new VaccinationRecord(person.Id, vaccineId, DateTime.UtcNow, 1);

		Action act = () => person.AddVaccination(record2);

		act.Should().Throw<DomainException>();
	}
}
