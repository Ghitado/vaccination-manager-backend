using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class Person
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public List<VaccinationRecord> VaccinationRecords { get; private set; } = [];

	private Person() { }

	public Person(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Name is required.");

		Id = Guid.NewGuid();
		Name = name;
	}

	public void AddVaccination(VaccinationRecord record)
	{
		if (record == null)
			throw new DomainException("VaccinationRecord is required.");

		bool alreadyTaken = VaccinationRecords
			.Any(v => v.VaccineId == record.VaccineId && v.Dose == record.Dose);

		if (alreadyTaken)
			throw new DomainException("This dose for this vaccine has already been registered.");

		VaccinationRecords.Add(record);
	}
}
