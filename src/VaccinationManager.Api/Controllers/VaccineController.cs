using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Application.UseCases.Vaccines.Create;
using VaccinationManager.Application.UseCases.Vaccines.GetPaginated;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccineController : ControllerBase
{
	/// <summary>
	/// Creates a new vaccine.
	/// </summary>
	/// <param name="request">Create vaccine request.</param>
	/// <param name="useCase"></param>
	/// <returns>The created vaccine.</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(VaccineResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Create vaccine",
		Description = "Creates a new vaccine and returns the created resource.")]
	public async Task<ActionResult<VaccineResponse>> Create(
		[FromBody] CreateVaccineRequest request,
		[FromServices] ICreateVaccineUseCase useCase)
	{
		var response = await useCase.Execute(request);

		return Created(string.Empty, response);
	}

	/// <summary>
	/// Returns a paginated list of vaccines.
	/// </summary>
	/// <param name="pageNumber">Page number (optional, defaults applied by use case/repository).</param>
	/// <param name="pageSize">Page size (optional, defaults applied by use case/repository).</param>
	/// <param name="useCase"></param>
	/// <returns>Paginated list of vaccines.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<VaccineResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Get paginated vaccines",
		Description = "Returns a paginated list of vaccines.")]
	public async Task<ActionResult<PaginatedResult<VaccineResponse>>> GetAll(
		[FromQuery] int pageNumber,
		[FromQuery] int pageSize,
		[FromServices] IGetPaginatedVaccinesUseCase useCase)
	{
		var response = await useCase.Execute(pageNumber, pageSize);
		return Ok(response);
	}
}
