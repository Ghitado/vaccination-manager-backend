using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			.HasMaxLength(100)
			.IsRequired();
		
		builder.HasMany(p => p.VaccinationRecords)
			.WithOne(r => r.Person)
			.HasForeignKey(r => r.PersonId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
