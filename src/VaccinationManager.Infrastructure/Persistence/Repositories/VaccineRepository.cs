using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class VaccineRepository : IVaccineRepository
{
	private readonly VaccinationManagerDbContext _context;

	public VaccineRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<PaginatedResult<Vaccine>> GetPaginated(int pageNumber = 1, int pageSize = 10)
	{
		var total = await _context.Vaccines.CountAsync();

		var items = await _context.Vaccines
			.AsNoTracking()
			.OrderBy(v => v.Name)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return new PaginatedResult<Vaccine>(items, total, pageNumber, pageSize);
	}

	public async Task<Vaccine?> GetById(Guid id) =>
		await _context.Vaccines
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Id == id);

	public async Task<Vaccine> Add(Vaccine vaccine)
	{
		_context.Vaccines.Add(vaccine);
		await _context.SaveChangesAsync();
		return vaccine;
	}
}

