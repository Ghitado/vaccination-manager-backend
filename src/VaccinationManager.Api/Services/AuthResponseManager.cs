using VaccinationManager.Application.Dtos.Auth;

namespace VaccinationManager.Api.Services;

public class AuthResponseManager : IAuthResponseManager
{
	private const string RefreshTokenCookieKey = "x-refresh-token";

	public void SetTokensAndPrepareResponse(LoginResponse response, HttpContext context)
	{
		context.Response.Cookies.Append(
			RefreshTokenCookieKey,
			response.RefreshToken,
			new CookieOptions
			{
				HttpOnly = true,
				Secure = true,   
				Expires = DateTimeOffset.UtcNow.AddDays(7),
				SameSite = SameSiteMode.Strict
			}
		);
	}
}

