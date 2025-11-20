using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Application.UseCases.VaccinationRecords.Create;
using VaccinationManager.Application.UseCases.VaccinationRecords.Delete;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetAll;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetById;
using VaccinationManager.Application.UseCases.VaccinationRecords.GetByPersonId;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VaccinationRecordController : ControllerBase
{
	/// <summary>
	/// Creates a new vaccination record.
	/// </summary>
	/// <param name="request">Create vaccination record request</param>
	/// <param name="useCase"></param>
	/// <returns>Created vaccination record</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(VaccinationRecordResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Create vaccination record",
		Description = "Creates a new vaccination record and returns the created resource.")]
	public async Task<ActionResult<VaccinationRecordResponse>> Create(
		[FromBody] CreateVaccinationRecordRequest request,
		[FromServices] ICreateVaccinationRecordUseCase useCase)
	{
		var response = await useCase.Execute(request);

		return CreatedAtAction(nameof(GetById), new { response.Id }, response);
	}

	/// <summary>
	/// Returns vaccination records for a given person.
	/// </summary>
	/// <param name="id">Person id.</param>
	/// <param name="pageNumber"></param>
	/// <param name="pageSize"></param>
	/// <param name="useCase"></param>
	/// <returns>Paginated vaccination records for the person.</returns>
	[HttpGet("person/{id}")]
	[ProducesResponseType(typeof(PaginatedResult<VaccinationRecordResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get vaccination records by person",
		Description = "Returns paginated vaccination records for a specific person.")]
	public async Task<ActionResult<PaginatedResult<VaccinationRecordResponse>>> GetByPersonId(
		[FromRoute] Guid id,
		[FromQuery] int? pageNumber,
		[FromQuery] int? pageSize,
		[FromServices] IGetAllVaccinationRecordsByPersonIdUseCase useCase)
	{
		var response = await useCase.Execute(id, pageNumber, pageSize);

		return Ok(response);
	}

	/// <summary>
	/// Returns a vaccination record by id.
	/// </summary>
	/// <param name="id">Vaccination record id.</param>
	/// <param name="useCase"></param>
	/// <returns>The vaccination record if found, otherwise 404.</returns>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(VaccinationRecordResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get vaccination record by id",
		Description = "Returns a single vaccination record by id.")]
	public async Task<ActionResult<VaccinationRecordResponse>> GetById(
		[FromRoute] Guid id,
		[FromServices] IGetVaccinationRecordByIdUseCase useCase)
	{
		var response = await useCase.Execute(id);
		return Ok(response);
	}

	/// <summary>
	/// Deletes a vaccination record by id.
	/// </summary>
	/// <param name="id">Vaccination record id.</param>
	/// <param name="useCase"></param>
	/// <returns>No content on success, 404 if not found.</returns>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Delete vaccination record",
		Description = "Deletes a vaccination record by id.")]
	public async Task<ActionResult> DeleteById(
		[FromRoute] Guid id,
		[FromServices] IDeleteVaccinationRecordByIdUseCase useCase)
	{
		await useCase.Execute(id);
		return Ok();
	}

	/// <summary>
	/// Returns a paginated list of vaccination records.
	/// </summary>
	/// <param name="pageNumber">Page number (optional, defaults applied by use case/repository).</param>
	/// <param name="pageSize">Page size (optional, defaults applied by use case/repository).</param>
	/// <param name="useCase"></param>
	/// <returns>Paginated list of vaccination records.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<VaccinationRecordResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Get all vaccination records",
		Description = "Returns a paginated list of vaccination records.")]
	public async Task<ActionResult<PaginatedResult<VaccinationRecordResponse>>> GetAll(
		[FromQuery] int? pageNumber, 
		[FromQuery] int? pageSize,
		[FromServices] IGetAllVaccinationRecordsUseCase useCase)
	{
		var response = await useCase.Execute(pageNumber, pageSize);
		return Ok(response);
	}
}
