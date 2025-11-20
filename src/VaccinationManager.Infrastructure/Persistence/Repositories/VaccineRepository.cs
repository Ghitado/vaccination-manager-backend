using Microsoft.EntityFrameworkCore;
using System.Linq;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class VaccineRepository : IVaccineRepository
{
	private readonly VaccinationManagerDbContext _context;

	public VaccineRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<PaginatedResult<Vaccine>> GetAll(int? pageNumber, int? pageSize)
	{
		var pageNumberDefault = pageNumber ?? 1;
		var pageSizeDefault = pageSize ?? 10;

		var total = await _context.Vaccines.CountAsync();
		var items = await _context.Vaccines
			.OrderBy(v => v.Name)
			.Skip((pageNumberDefault - 1) * pageSizeDefault)
			.Take(pageSizeDefault)
			.ToListAsync();

		return new PaginatedResult<Vaccine>(items, total, pageNumberDefault, pageSizeDefault);
	}

	public async Task<Vaccine?> GetById(Guid id) =>
		await _context.Vaccines.FirstOrDefaultAsync(v => v.Id == id);

	public async Task<Vaccine> Add(Vaccine vaccine)
	{
		_context.Vaccines.Add(vaccine);
		await _context.SaveChangesAsync();

		return vaccine;
	}
}

