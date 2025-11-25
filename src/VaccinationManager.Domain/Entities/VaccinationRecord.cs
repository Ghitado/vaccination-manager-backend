using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class VaccinationRecord
{
	public Guid Id { get; private set; }
	public Guid PersonId { get; private set; }
	public Guid VaccineId { get; private set; }
	public DateTime AppliedAt { get; private set; }
	public int Dose { get; private set; }

	public Person? Person { get; private set; }
	public Vaccine? Vaccine { get; private set; }

	private VaccinationRecord() { }

	public VaccinationRecord(Guid personId, Guid vaccineId, DateTime appliedAt, int dose)
	{
		var appliedAtUtc = appliedAt.ToUniversalTime();

		if (personId == Guid.Empty)
			throw new DomainException("PersonId is required.");

		if (vaccineId == Guid.Empty)
			throw new DomainException("VaccineId is required.");

		if (appliedAtUtc == default)
			throw new DomainException("Invalid date.");

		if (appliedAtUtc > DateTime.UtcNow)
			throw new DomainException("Vaccination date cannot be in the future.");

		if (dose is <= 0)
			throw new DomainException("Invalid dose.");

		Id = Guid.NewGuid();
		PersonId = personId;
		VaccineId = vaccineId;
		AppliedAt = appliedAtUtc;
		Dose = dose;
	}
}
