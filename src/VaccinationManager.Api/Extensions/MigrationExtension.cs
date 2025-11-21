using Microsoft.EntityFrameworkCore;
using VaccinationManager.Infrastructure.Persistence;

namespace VaccinationManager.Api.Extensions;

public static class MigrationExtension
{
	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<VaccinationManagerDbContext>();

		var pendingMigrations = dbContext.Database.GetPendingMigrations();

		if (pendingMigrations.Any())
			dbContext.Database.Migrate();
	}
}

