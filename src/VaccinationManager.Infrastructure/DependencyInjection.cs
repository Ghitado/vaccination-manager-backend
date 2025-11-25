using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Domain.Security.Cryptography;
using VaccinationManager.Domain.Security.Tokens;
using VaccinationManager.Infrastructure.Persistence;
using VaccinationManager.Infrastructure.Persistence.Repositories;
using VaccinationManager.Infrastructure.Security.Cryptography;
using VaccinationManager.Infrastructure.Security.Tokens;

namespace VaccinationManager.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		AddRepositories(services);
		AddDbContext_PostgreSql(services, configuration);
		AddTokens(services);
		AddPasswordHasher(services);
	}

	private static void AddDbContext_PostgreSql(IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<VaccinationManagerDbContext>(options =>
				options.UseNpgsql(connectionString));
	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
		services.AddScoped<IVaccineRepository, VaccineRepository>();
		services.AddScoped<IPersonRepository, PersonRepository>();
		services.AddScoped<IVaccinationRecordRepository, VaccinationRecordRepository>();
	}

	private static void AddTokens(IServiceCollection services)
	{
		services.AddScoped<ITokenService, JwtTokenService>();
	}

	private static void AddPasswordHasher(IServiceCollection services)
	{
		services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
	}
}

