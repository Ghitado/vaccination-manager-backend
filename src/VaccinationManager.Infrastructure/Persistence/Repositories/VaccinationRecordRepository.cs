using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class VaccinationRecordRepository : IVaccinationRecordRepository
{
	private readonly VaccinationManagerDbContext _context;

	public VaccinationRecordRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<PaginatedResult<VaccinationRecord>> GetAll(int? pageNumber, int? pageSize)
	{
		var pageNumberDefault = pageNumber ?? 1;
		var pageSizeDefault = pageSize ?? 10;

		var total = await _context.VaccinationRecords.CountAsync();
		var items = await _context.VaccinationRecords
			.Include(r => r.Person)
			.Include(r => r.Vaccine)
			.OrderBy(r => r.Date) 
			.Skip((pageNumberDefault - 1) * pageSizeDefault)
			.Take(pageSizeDefault)
			.ToListAsync();

		return new PaginatedResult<VaccinationRecord>(items, total, pageNumberDefault, pageSizeDefault);
	}

	public async Task<VaccinationRecord?> GetById(Guid id) =>
		await _context.VaccinationRecords
			.Include(r => r.Person)
			.Include(r => r.Vaccine)
			.FirstOrDefaultAsync(r => r.Id == id);

	public async Task<PaginatedResult<VaccinationRecord>> GetAllByPersonId(Guid personId, int? pageNumber, int? pageSize)
	{
		var pageNumberDefault = pageNumber ?? 1;
		var pageSizeDefault = pageSize ?? 10;

		var total = await _context.VaccinationRecords.CountAsync();
		var items = await _context.VaccinationRecords
				.Where(r => r.PersonId == personId)
				.Include(r => r.Person)
				.Include(r => r.Vaccine)
				.OrderBy(r => r.Date)
				.Skip((pageNumberDefault - 1) * pageSizeDefault)
				.Take(pageSizeDefault)
				.ToListAsync();

		return new PaginatedResult<VaccinationRecord>(items, total, pageNumberDefault, pageSizeDefault);
	}

	public async Task<VaccinationRecord> Add(VaccinationRecord record)
	{
		_context.VaccinationRecords.Add(record);
		await _context.SaveChangesAsync();

		return record;
	}

	public async Task Delete(VaccinationRecord record)
	{
		_context.VaccinationRecords.Remove(record);
		await _context.SaveChangesAsync();
	}
}
