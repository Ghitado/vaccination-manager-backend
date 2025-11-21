using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.Persons;
using VaccinationManager.Application.UseCases.Persons.Create;
using VaccinationManager.Application.UseCases.Persons.Delete;
using VaccinationManager.Application.UseCases.Persons.GetPaginated;
using VaccinationManager.Application.UseCases.Persons.GetById;
using VaccinationManager.Domain.Common;

namespace VaccinationManager.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
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
		var response = await useCase.Execute(request);

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
		var response = await useCase.Execute(pageNumber, pageSize);

		return Ok(response);
	}

	/// <summary>
	/// Returns paginated vaccinatio records of a single person by id.
	/// </summary>
	/// <param name="id">Person id.</param>
	/// <param name="useCase"></param>
	/// <returns>The person if found, otherwise 404.</returns>
	[HttpGet("{id}/vaccinationrecords")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get vaccination records by person id",
		Description = "Returns paginated vaccinatio records of a single person by id.")]
	public async Task<ActionResult> GetById(
		[FromRoute] Guid id,
		[FromServices] IGetPersonByIdUseCase useCase)
	{
		var response = await useCase.Execute(id);

		return response is not null ? Ok(response) : NotFound();
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
		Summary = "Get person by id",
		Description = "Deletes a person by id.")]
	public async Task<ActionResult> DeleteById(
		[FromRoute] Guid id,
		[FromServices] IDeletePersonByIdUseCase useCase)
	{
		await useCase.Execute(id);
		return Ok();
	}
}
