using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationManager.Application.Dtos.VaccinationRecords;
using VaccinationManager.Application.UseCases.VaccinationRecords.Create;
using VaccinationManager.Application.UseCases.VaccinationRecords.Delete;

namespace VaccinationManager.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccinationRecordController : ControllerBase
{
	private readonly ILogger<VaccinationRecordController> _logger;

	public VaccinationRecordController(ILogger<VaccinationRecordController> logger)
	{
		_logger = logger;
	}

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
		_logger.LogInformation("Registering vaccination. Person: {PersonId}, Vaccine: {VaccineId}", request.PersonId, request.VaccineId);

		var response = await useCase.Execute(request);

		_logger.LogInformation("Vaccination record created successfully. ID: {Id}", response.Id);

		return Created(string.Empty, response);
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
		_logger.LogInformation("Request to delete vaccination record. ID: {Id}", id);

		await useCase.Execute(id);

		_logger.LogInformation("Vaccination record deleted successfully. ID: {Id}", id);
		return Ok();
	}
}
