using VaccinationManager.Domain.Common;

namespace VaccinationManager.Domain.Entities;

public sealed class Vaccine
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	private Vaccine() { }

	public Vaccine(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Name is required.");

		Id = Guid.NewGuid();
		Name = name;
	}
}
