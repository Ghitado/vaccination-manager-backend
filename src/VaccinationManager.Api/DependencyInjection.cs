using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using VaccinationManager.Api.Services;

namespace VaccinationManager.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
	{
		AddAuthentication(services, configuration);
		AddSwagger(services);
		AddServices(services);

		return services;
	}

	private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
	{
		var jwtKey = configuration["JWT_KEY"] ?? throw new InvalidOperationException("JWT_KEY not set.");

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
				ClockSkew = TimeSpan.Zero
			};
		});
	}

	private static void AddSwagger(IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Vaccination Manager API",
				Version = "v1",
				Description = "API for managing persons, vaccines and vaccination records."
			});

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme.",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "bearer"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
					},
					Array.Empty<string>()
				}
			});

			c.EnableAnnotations();

			c.MapType<DateTime>(() => new OpenApiSchema
			{
				Type = "string",
				Format = "date-time",
				Example = new Microsoft.OpenApi.Any.OpenApiString(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "-03:00")
			});

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
		});
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<IAuthResponseManager, AuthResponseManager>();
	}
}

