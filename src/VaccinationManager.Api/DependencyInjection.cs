using VaccinationManager.Api.Services;

namespace VaccinationManager.Api;

public static class DependencyInjection
{
	public static void AddApi(this IServiceCollection services)
	{
		AddServices(services);
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<IAuthResponseManager, AuthResponseManager>();
	}
}

