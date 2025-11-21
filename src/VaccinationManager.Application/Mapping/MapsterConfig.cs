using Mapster;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Domain.Entities;

namespace VaccinationManager.Application.Mapping;

public class MapsterConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<VaccinationRecord, VaccinationRecordResponse>()
			.Map(dest => dest.VaccineName, src => src.Vaccine!.Name);
	}
}

