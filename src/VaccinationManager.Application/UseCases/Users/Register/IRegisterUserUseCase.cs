using VaccinationManager.Application.Dtos.Users;

namespace VaccinationManager.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
	Task Execute(RegisterRequest request);
}
