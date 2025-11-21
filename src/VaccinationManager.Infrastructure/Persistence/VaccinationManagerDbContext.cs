using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence;

public class VaccinationManagerDbContext : IdentityDbContext<IdentityUser>
{
	public VaccinationManagerDbContext(DbContextOptions<VaccinationManagerDbContext> options)
		: base(options) { }

	public DbSet<Person> Persons { get; set; }
	public DbSet<Vaccine> Vaccines { get; set; }
	public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder); 
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VaccinationManagerDbContext).Assembly);
	}
}

