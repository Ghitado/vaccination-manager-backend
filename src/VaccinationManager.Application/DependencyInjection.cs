using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VaccinationManager.Application.UseCases.Login.DoLogin;
using VaccinationManager.Application.UseCases.Persons.Create;
using VaccinationManager.Application.UseCases.Persons.Delete;
using VaccinationManager.Application.UseCases.Persons.GetById;
using VaccinationManager.Application.UseCases.Persons.GetPaginated;
using VaccinationManager.Application.UseCases.Tokens.RefreshToken;
using VaccinationManager.Application.UseCases.Users.Register;
using VaccinationManager.Application.UseCases.VaccinationRecords.Create;
using VaccinationManager.Application.UseCases.VaccinationRecords.Delete;
using VaccinationManager.Application.UseCases.Vaccines.Create;
using VaccinationManager.Application.UseCases.Vaccines.GetPaginated;

namespace VaccinationManager.Application;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		AddMapsterConfig();
		AddUseCases(services);
	}

	private static void AddMapsterConfig()
	{
		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
	}

	private static void AddUseCases(IServiceCollection services)
	{
		services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
		services.AddScoped<ILoginUseCase, LoginUseCase>();

		services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

		services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
		services.AddScoped<IGetPaginatedPersonsUseCase, GetPaginatedPersonsUseCase>();
		services.AddScoped<IGetPersonByIdUseCase, GetPersonByIdUseCase>();
		services.AddScoped<IDeletePersonByIdUseCase, DeletePersonByIdUseCase>();

		services.AddScoped<ICreateVaccineUseCase, CreateVaccineUseCase>();
		services.AddScoped<IGetPaginatedVaccinesUseCase, GetPaginatedVaccinesUseCase>();

		services.AddScoped<ICreateVaccinationRecordUseCase, CreateVaccinationRecordUseCase>();
		services.AddScoped<IDeleteVaccinationRecordByIdUseCase, DeleteVaccinationRecordByIdUseCase>();
	}
}

