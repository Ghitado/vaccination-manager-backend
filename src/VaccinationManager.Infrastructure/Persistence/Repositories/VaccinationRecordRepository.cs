using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class VaccinationRecordRepository : IVaccinationRecordRepository
{
	private readonly VaccinationManagerDbContext _context;

	public VaccinationRecordRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<VaccinationRecord> Add(VaccinationRecord record)
	{
		_context.VaccinationRecords.Add(record);
		await _context.SaveChangesAsync();
		return record;
	}

	public async Task<VaccinationRecord?> GetById(Guid id) =>
		await _context.VaccinationRecords
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Id == id);

	public async Task Delete(VaccinationRecord record)
	{
		_context.VaccinationRecords.Remove(record);
		await _context.SaveChangesAsync();
	}
}
