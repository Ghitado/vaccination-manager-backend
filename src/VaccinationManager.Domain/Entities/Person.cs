using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class Person
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	public List<VaccinationRecord> VaccinationRecords { get; private set; } = new();

	private Person() { }

	public Person(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Name is required.");

		Id = Guid.NewGuid();
		Name = name;
	}

	public void UpdateName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Name is required.");

		Name = name;
	}
}
