using System.ComponentModel.DataAnnotations;

namespace VaccinationManager.Application.Attributes;

public class NotFutureAttribute : ValidationAttribute
{
	public override bool IsValid(object? value)
	{
		if (value is DateTime date)
			return date <= DateTime.Now;

		return true;
	}
}

