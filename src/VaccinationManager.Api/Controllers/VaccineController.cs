using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.Vaccines;
using VaccinationManager.Application.UseCases.Persons.Delete;
using VaccinationManager.Application.UseCases.VaccinationRecords.Delete;
using VaccinationManager.Application.UseCases.Vaccines.Create;
using VaccinationManager.Application.UseCases.Vaccines.GetAll;
using VaccinationManager.Application.UseCases.Vaccines.GetById;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Api.Controllers;
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

		return CreatedAtAction(nameof(GetById), new { response.Id }, response);
	}

	/// <summary>
	/// Returns a vaccine by id.
	/// </summary>
	/// <param name="id">Vaccine id.</param>
	/// <param name="useCase"></param>
	/// <returns>The vaccine if found, otherwise 404.</returns>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(VaccineResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get vaccine by id",
		Description = "Returns a single vaccine by id.")]
	public async Task<ActionResult<VaccineResponse?>> GetById(
		[FromRoute] Guid id,
		[FromServices] IGetVaccineByIdUseCase useCase)
	{
		var response = await useCase.Execute(id);

		return response is not null ? Ok(response): NotFound(); 
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
		Summary = "Get all vaccines",
		Description = "Returns a paginated list of vaccines.")]
	public async Task<ActionResult<PaginatedResult<VaccineResponse>>> GetAll(
		[FromQuery] int? pageNumber,
		[FromQuery] int? pageSize,
		[FromServices] IGetAllVaccinesUseCase useCase)
	{
		var response = await useCase.Execute(pageNumber, pageSize);
		return Ok(response);
	}
}
