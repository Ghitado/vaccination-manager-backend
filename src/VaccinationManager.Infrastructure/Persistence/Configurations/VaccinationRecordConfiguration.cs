using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence.Configurations;

public class VaccinationRecordConfiguration : IEntityTypeConfiguration<VaccinationRecord>
{
	public void Configure(EntityTypeBuilder<VaccinationRecord> builder)
	{
		builder.HasKey(r => r.Id);

		builder.Property(r => r.Date)
			.IsRequired();
		
		builder.Property(r => r.Dose)
			.IsRequired();
		
		builder.HasOne(r => r.Person)
			.WithMany(p => p.VaccinationRecords)
			.HasForeignKey(r => r.PersonId);
		
		builder.HasOne(r => r.Vaccine)
			.WithMany()
			.HasForeignKey(r => r.VaccineId);
	}
}

