using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Security.Cryptography;
using VaccinationManager.Domain.Security.Tokens;

namespace VaccinationManager.Infrastructure.Security.Tokens;

public class JwtTokenService : ITokenService
{
	private readonly IConfiguration _config;
	private readonly IPasswordHasher _passwordHasher;

	public JwtTokenService(IConfiguration config, IPasswordHasher passwordHasher)
	{
		_config = config;
		_passwordHasher = passwordHasher;
	}

	public string CreateToken(User user)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_KEY"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!)
		};

		var token = new JwtSecurityToken(
			expires: DateTime.UtcNow.AddMinutes(60),
			claims: claims,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public string GenerateRefreshToken() => Guid.NewGuid().ToString("N");
	public string HashToken(string token) => _passwordHasher.HashPassword(token);
}