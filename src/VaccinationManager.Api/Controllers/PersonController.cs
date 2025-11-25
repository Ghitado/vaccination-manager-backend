using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Application.UseCases.Persons.Create;
using VaccinationManager.Application.UseCases.Persons.Delete;
using VaccinationManager.Application.UseCases.Persons.GetById;
using VaccinationManager.Application.UseCases.Persons.GetPaginated;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
	private readonly ILogger<PersonController> _logger;

	public PersonController(ILogger<PersonController> logger)
	{
		_logger = logger;
	}

	/// <summary>
	/// Creates a new person.
	/// </summary>
	/// <param name="request">Create person request.</param>
	/// <param name="useCase"></param>
	/// <returns>The created person.</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Create person",
		Description = "Creates a new person and returns the created resource.")]
	public async Task<ActionResult<PersonResponse>> Create(
		[FromBody] CreatePersonRequest request,
		[FromServices] ICreatePersonUseCase useCase)
	{
		_logger.LogInformation("Starting creation process for person: {Name}", request.Name);

		var response = await useCase.Execute(request);

		_logger.LogInformation("Person created successfully. ID: {Id}", response.Id);

		return CreatedAtAction(nameof(GetById), new { response.Id }, response);
	}

	/// <summary>
	/// Returns a paginated list of persons.
	/// </summary>
	/// <param name="pageNumber">Page number (optional, defaults applied by use case/repository).</param>
	/// <param name="pageSize">Page size (optional, defaults applied by use case/repository).</param>
	/// <param name="useCase"></param>
	/// <returns>Paginated list of persons.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<PaginatedPersonResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Get paginated persons",
		Description = "Returns a paginated list of persons.")]
	public async Task<ActionResult> GetPaginated(
		[FromQuery] int pageNumber, 
		[FromQuery] int pageSize,
		[FromServices] IGetPaginatedPersonsUseCase useCase)
	{
		_logger.LogInformation("Fetching persons. Page: {Page}, Size: {Size}", pageNumber, pageSize);

		var response = await useCase.Execute(pageNumber, pageSize);

		return Ok(response);
	}

	/// <summary>
	/// Returns paginated vaccination records of a single person by id.
	/// </summary>
	/// <param name="id">Person id.</param>
	/// <param name="useCase"></param>
	/// <returns>The person if found, otherwise 404.</returns>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get vaccination records by person id",
		Description = "Returns paginated vaccination records of a single person by id.")]
	public async Task<ActionResult> GetById(
		[FromRoute] Guid id,
		[FromServices] IGetPersonByIdUseCase useCase)
	{
		_logger.LogInformation("Fetching person details. ID: {Id}", id);

		var response = await useCase.Execute(id);

		if (response is null)
		{
			_logger.LogWarning("Person not found. ID: {Id}", id);
			return NotFound();
		}

		return Ok(response);
	}

	/// <summary>
	/// Deletes a person by id.
	/// </summary>
	/// <param name="id">Person id.</param>
	/// <param name="useCase"></param>
	/// <returns>The person if found, otherwise 404.</returns>
	[HttpDelete("{id}")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Deletes a person by id",
		Description = "Deletes a person by id.")]
	public async Task<ActionResult> DeleteById(
		[FromRoute] Guid id,
		[FromServices] IDeletePersonByIdUseCase useCase)
	{
		_logger.LogInformation("Request to delete person. ID: {Id}", id);

		await useCase.Execute(id);

		_logger.LogInformation("Person deleted successfully. ID: {Id}", id);
		return Ok();
	}
}
