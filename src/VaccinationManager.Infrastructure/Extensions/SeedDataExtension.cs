using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;
using VaccinationManager.Domain.Security.Cryptography;
using VaccinationManager.Infrastructure.Persistence;

namespace VaccinationManager.Infrastructure.Extensions;

public static class SeedDataExtension
{
	private const string AdminEmail = "admin@vaccin.app";
	private const string AdminPassword = "Password123@"; 

	public static async Task SeedInitialDataAsync(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();

		var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
		var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
		var dbContext = scope.ServiceProvider.GetRequiredService<VaccinationManagerDbContext>();

		var adminExists = await userRepository.FindByEmailAsync(AdminEmail);

		if (adminExists is null)
		{
			var passwordHash = passwordHasher.HashPassword(AdminPassword);
			var adminUser = new User(AdminEmail, passwordHash);

			await userRepository.AddAsync(adminUser);
		}

		if (!await dbContext.Vaccines.AnyAsync())
		{
			var vaccines = new List<Vaccine>
			{
				new Vaccine("BCG"),
				new Vaccine("Hepatitis B"),
				new Vaccine("Polio (SABIN)"),
				new Vaccine("COVID-19 (Pfizer)")
			};
			await dbContext.Vaccines.AddRangeAsync(vaccines);
		}

		await dbContext.SaveChangesAsync();
	}
}

