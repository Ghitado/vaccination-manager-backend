using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence.Configurations;

public class VaccinationRecordConfiguration : IEntityTypeConfiguration<VaccinationRecord>
{
	public void Configure(EntityTypeBuilder<VaccinationRecord> builder)
	{
		builder.ToTable("VaccinationRecords");

		builder.HasKey(r => r.Id);

		builder.Property(r => r.AppliedAt)
			.IsRequired();

		builder.Property(r => r.Dose)
			.IsRequired();

		builder.HasOne(r => r.Person)
			.WithMany(p => p.VaccinationRecords)
			.HasForeignKey(r => r.PersonId)
			.OnDelete(DeleteBehavior.Cascade); 

		builder.HasOne(r => r.Vaccine)
			.WithMany()
			.HasForeignKey(r => r.VaccineId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

