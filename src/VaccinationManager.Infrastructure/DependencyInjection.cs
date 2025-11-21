using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Infrastructure.Persistence;
using VaccinationManager.Infrastructure.Persistence.Repositories;

namespace VaccinationManager.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Database");

		services.AddDbContext<VaccinationManagerDbContext>(options =>
			options.UseSqlite(connectionString));

		services.AddScoped<IPersonRepository, PersonRepository>();
		services.AddScoped<IVaccineRepository, VaccineRepository>();
		services.AddScoped<IVaccinationRecordRepository, VaccinationRecordRepository>();
	}
}

