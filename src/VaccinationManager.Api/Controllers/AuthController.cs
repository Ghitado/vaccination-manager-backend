using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Authentication;
using VaccinationManager.Api.Services;
using VaccinationManager.Application.Dtos.Auth;
using VaccinationManager.Application.Dtos.Login;
using VaccinationManager.Application.Dtos.Users;
using VaccinationManager.Application.UseCases.Login.DoLogin;
using VaccinationManager.Application.UseCases.Tokens.RefreshToken;
using VaccinationManager.Application.UseCases.Users.Register;

namespace VaccinationManager.Api.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly ILogger<AuthController> _logger;
	private readonly IAuthResponseManager _responseManager;

	public AuthController(ILogger<AuthController> logger, IAuthResponseManager responseManager)
	{
		_logger = logger;
		_responseManager = responseManager;
	}

	/// <summary>
	/// Registers a new user account.
	/// </summary>
	/// <param name="request">Email and password for the new user.</param>
	/// <param name="useCase"></param>
	/// <returns>HTTP 201 Created on success.</returns>
	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(Summary = "Register new user", Description = "Creates a new user account.")]
	public async Task<IActionResult> Register(
		[FromBody] RegisterRequest request,
		[FromServices] IRegisterUserUseCase useCase)
	{
		await useCase.Execute(request);

		_logger.LogInformation("User registered successfully: {Email}", request.Email);
		return StatusCode(StatusCodes.Status201Created);
	}

	/// <summary>
	/// Authenticates a user and issues JWT and Refresh Token.
	/// </summary>
	/// <param name="request">Email and password for login.</param>
	/// <param name="useCase"></param>
	/// <returns>The access token and refresh token in the response body.</returns>
	[HttpPost("login")]
	[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[SwaggerOperation(Summary = "User login and token issuance", Description = "Authenticates user and returns tokens.")]
	public async Task<ActionResult<LoginResponse>> Login(
		[FromBody] LoginRequest request,
		[FromServices] ILoginUseCase useCase)
	{
		_logger.LogInformation("Attempting login for user: {Email}", request.Email);

		var response = await useCase.Execute(request);

		_responseManager.SetTokensAndPrepareResponse(response, HttpContext);

		_logger.LogInformation("Login successful for user: {Email}", request.Email);
		return Ok(response);
	}

	/// <summary>
	/// Renews access token using a valid Refresh Token cookie.
	/// </summary>
	/// <param name="useCase"></param>
	/// <param name="refreshTokenCookie">The Refresh Token string is expected to be present in the 'x-refresh-token' HttpOnly cookie header.</param>
	/// <returns>A new pair of access and refresh tokens.</returns>
	[HttpPost("refresh")]
	[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[SwaggerOperation(Summary = "Refresh token renewal", Description = "Renews access token using the Refresh Token cookie.")]
	public async Task<ActionResult<LoginResponse>> Refresh(
		[FromServices] IRefreshTokenUseCase useCase,
		[FromBody] RefreshTokenRequest? request = null)
	{
		var refreshTokenCookie = Request.Cookies["x-refresh-token"];

		var refreshToken = string.IsNullOrEmpty(refreshTokenCookie)
						   ? request?.RefreshToken
						   : refreshTokenCookie;

		if (string.IsNullOrEmpty(refreshToken))
		{
			_logger.LogWarning("Refresh attempt failed: Token missing from all sources.");
			return Unauthorized("Refresh token is missing.");
		}

		var newResponse = await useCase.Execute(refreshToken);

		_responseManager.SetTokensAndPrepareResponse(newResponse, HttpContext);

		_logger.LogInformation("Token successfully refreshed for user ID in token.");

		return Ok(newResponse);
	}
}