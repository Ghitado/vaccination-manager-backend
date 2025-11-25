using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Infrastructure.Persistence;

public class VaccinationManagerDbContext : DbContext
{
	public VaccinationManagerDbContext(DbContextOptions<VaccinationManagerDbContext> options)
		: base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
	public DbSet<Person> Persons { get; set; }
	public DbSet<Vaccine> Vaccines { get; set; }
	public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder); 
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VaccinationManagerDbContext).Assembly);
	}
}

