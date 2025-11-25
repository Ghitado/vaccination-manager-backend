using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence.Configurations;

public class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
{
	public void Configure(EntityTypeBuilder<Vaccine> builder)
	{
		builder.ToTable("Vaccines");

		builder.HasKey(v => v.Id);

		builder.Property(v => v.Name)
			.IsRequired()
			.HasMaxLength(100);
	}
}

