using Microsoft.Extensions.DependencyInjection;
using VaccinationManager.Application.UseCases.Persons.Create;
using VaccinationManager.Application.UseCases.Persons.Delete;
using VaccinationManager.Application.UseCases.Persons.GetAll;
using VaccinationManager.Application.UseCases.Persons.GetById;
using VaccinationManager.Application.UseCases.VaccinationRecords.Create;
using VaccinationManager.Application.UseCases.VaccinationRecords.Delete;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetAll;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetById;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetByPersonId;
using VaccinationManager.Application.UseCases.Vaccines.Create;
using VaccinationManager.Application.UseCases.Vaccines.GetAll;
using VaccinationManager.Application.UseCases.Vaccines.GetById;

namespace VaccinationManager.Application;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
		services.AddScoped<IGetAllPersonsUseCase, GetAllPersonsUseCase>();
		services.AddScoped<IGetPersonByIdUseCase, GetPersonByIdUseCase>();
		services.AddScoped<IDeletePersonByIdUseCase, DeletePersonByIdUseCase>();

		services.AddScoped<ICreateVaccineUseCase, CreateVaccineUseCase>();
		services.AddScoped<IGetAllVaccinesUseCase, GetAllVaccinesUseCase>();
		services.AddScoped<IGetVaccineByIdUseCase, GetVaccineByIdUseCase>();

		services.AddScoped<ICreateVaccinationRecordUseCase, CreateVaccinationRecordUseCase>();
		services.AddScoped<IGetAllVaccinationRecordsUseCase, GetAllVaccinationRecordsUseCase>();
		services.AddScoped<IGetAllVaccinationRecordsByPersonIdUseCase, GetAllVaccinationRecordsByPersonIdUseCase>();
		services.AddScoped<IGetVaccinationRecordByIdUseCase, GetVaccinationRecordByIdUseCase>();
		services.AddScoped<IDeleteVaccinationRecordByIdUseCase, DeleteVaccinationRecordByIdUseCase>();
	}
}

