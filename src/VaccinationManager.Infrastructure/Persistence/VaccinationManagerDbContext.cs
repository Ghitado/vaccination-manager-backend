using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence;

public class VaccinationManagerDbContext : DbContext
{
	public VaccinationManagerDbContext(DbContextOptions<VaccinationManagerDbContext> options)
		: base(options) { }

	public DbSet<Person> Persons { get; set; }
	public DbSet<Vaccine> Vaccines { get; set; }
	public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VaccinationManagerDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}

