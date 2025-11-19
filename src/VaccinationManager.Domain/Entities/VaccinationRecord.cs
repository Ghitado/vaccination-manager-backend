using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class VaccinationRecord
{
	public Guid Id { get; private set; }
	public Guid PersonId { get; private set; }
	public Guid VaccineId { get; private set; }
	public DateTime Date { get; private set; }
	public int Dose { get; private set; }

	public Person? Person { get; private set; }
	public Vaccine? Vaccine { get; private set; }

	private VaccinationRecord() { }

	public VaccinationRecord(Guid personId, Guid vaccineId, DateTime date, int dose)
	{
		if (personId == Guid.Empty)
			throw new DomainException("PersonId is required.");

		if (vaccineId == Guid.Empty)
			throw new DomainException("VaccineId is required.");

		if (date == default)
			throw new DomainException("Invalid date.");

		if (dose is <= 0)
			throw new DomainException("Invalid dose.");

		Id = Guid.NewGuid();
		PersonId = personId;
		VaccineId = vaccineId;
		Date = date;
		Dose = dose;
	}

	public void UpdateDose(int dose)
	{
		if (dose is <= 0)
			throw new DomainException("Invalid dose.");

		Dose = dose;
	}

	public void UpdateDate(DateTime date)
	{
		if (date == default)
			throw new DomainException("Invalid date.");

		Date = date;
	}
}
